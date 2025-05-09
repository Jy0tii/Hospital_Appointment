using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using ClinicBackendApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IContactUsRepository _contactUsRepository;

        public UserController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        //Post //api/User
        [HttpPost]
        public async Task<IActionResult> AddContactUs([FromBody] ContactUs contactUs)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newContactUs = await _contactUsRepository.AddContactUs(contactUs);
            return Ok(new { message = "contact us message sent successfully" });
        }
    }
}
