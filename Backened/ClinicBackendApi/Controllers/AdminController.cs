using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IContactUsRepository _contactUsRepository;

       public AdminController(IAppointmentRepository appointmentRepository, IContactUsRepository contactUsRepository)
        {
            _appointmentRepository = appointmentRepository;
            _contactUsRepository = contactUsRepository;
        }

        //GET: api/admin/userAppointments
        [HttpGet("userAppointments")]
        [Authorize (Roles = "Admin")] // Ensure the user is authenticated
        public async Task<ActionResult<IEnumerable<Appointment>>> GetUserAppointments(string id)
        {
            if (id == null) return BadRequest();

            // Fetch appointments for this user
            var appointments = await _appointmentRepository.GetUserAppointments(id);
            return Ok(appointments);
        }

                                                //Contact us
        //Get api/admin/userContactUs
        [HttpGet("userContactUs")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetContactUs()
        {
            return Ok(await _contactUsRepository.GetContactUs());
        }

        // DELETE: api/admin/userContactUs/5
        [HttpDelete("userContactUs/{id}")]
        public async Task<IActionResult> DeleteContactUs(int id)
        {
            var details = await _contactUsRepository.DeleteContactUs(id);
            if (!details) return NotFound();
            return NoContent();
        }
    }
}
