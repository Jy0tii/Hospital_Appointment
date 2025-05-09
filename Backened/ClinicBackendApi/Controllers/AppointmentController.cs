using System.Security.Claims;
using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointment()
        {
            return Ok(await _repository.GetAllAppointments());
        }

        //GET: api/appointment/userAppointments
        [HttpGet("userAppointments")]
        [Authorize(Roles = "User")] // Ensure the user is authenticated
        public async Task<ActionResult<IEnumerable<Appointment>>> GetUserAppointments()
        {
            // Get the current user's ID from JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            // Fetch appointments for this user
            var appointments = await _repository.GetUserAppointments(userId);

            return Ok(appointments);
        }

        // GET: api/appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _repository.GetAppointmentById(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        // POST: api/appointment
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newAppointment = await _repository.AddAppointment(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = newAppointment.Id }, newAppointment);
        }

        // PUT: api/appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedAppointment = await _repository.UpdateAppointment(appointment);
            if(!updatedAppointment) return NotFound();

            return Ok(updatedAppointment);
        }

        // DELETE: api/appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var deletedAppointment = await _repository.DeleteAppointment(id);
            if (!deletedAppointment) return NotFound();
            return NoContent();
        }
    }
}
