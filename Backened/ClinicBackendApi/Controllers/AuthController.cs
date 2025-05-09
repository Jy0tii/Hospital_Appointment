using ClinicBackendApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ClinicBackendApi.Interfaces;

namespace ClinicBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppointmentRepository _appointmentRepository;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration config,
            RoleManager<IdentityRole> roleManager, IAppointmentRepository appointmentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
            _appointmentRepository = appointmentRepository;
        }

        //Register check user exists or not
        [HttpGet("register/{email}")]
        public async Task<IActionResult> CheckUserRegistered(string email)
        {
            var userExits = await _userManager.FindByNameAsync(email);
            if (userExits != null) return Ok(new{message = "email already exits"});

            return Ok(new { message = "User not registered" });
        }

        //Register /api/auth/register/
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PinCode = model.PinCode,
                MedicalHistory = model.MedicalHistory,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            //Add default role "User"
            var role = string.IsNullOrWhiteSpace(model.Role) ? "User" : (model.Role);

            // in case role doesn't exist

            //if (!await _roleManager.RoleExistsAsync(role))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(role));
            //}
            await _userManager.AddToRoleAsync(user, role);

            return Ok(new { message = "User registered successfully" });
        }

        //Login /api/auth/
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) return NotFound("Not registered");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Password wrong");

            var roles = await _userManager.GetRolesAsync(user);

            //Generate token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Id", user.Id),
                new Claim("Name", user.Name),
                new Claim("PhoneNumber", user.PhoneNumber),
        };
            foreach (var role in roles)
            {
                //claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add (new Claim("Role", role));
            }

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );


            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }


        //Get all users  /api/auth
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null) return NotFound("No users registered");

            var userList = new List<object>();
            foreach(var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role[0] == "User")
                {
                    userList.Add(new
                    {
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Gender,
                        user.DateOfBirth,
                        user.Address,
                        user.City,
                        user.State,
                        user.PinCode,
                        user.PhoneNumber,
                        user.MedicalHistory,
                        Roles = role,
                    });
                }

                //// Eager load User with each Appointment
                //var appointments = _context.Appointments
                //    .Include(a => a.User)
                //    .ToList();

                //foreach (var appointment in appointments)
                //{
                //    Console.WriteLine($"Appointment by: {appointment.User.UserName}");
                //}

            }
            return Ok(userList);
        }

        //Get user By id /api/auth
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("user not found");

            var role = await _userManager.GetRolesAsync(user);
            return Ok(new {
                user.Id,
                user.Name,
                user.Email,
                user.Gender,
                user.DateOfBirth,
                user.Address,
                user.City,
                user.State,
                user.PinCode,
                user.PhoneNumber,
                user.MedicalHistory,
                Roles = role,
            });
        }

        //update user by Id /api/auth
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RegisterDto model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("user not found");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            //update fields
            user.UserName = model.Email;
            user.Email = model.Email;
            user.Name = model.Name;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.Address = model.Address;
            user.City = model.City;
            user.State = model.State;
            user.PinCode = model.PinCode;
            user.MedicalHistory = model.MedicalHistory;
            user.PhoneNumber = model.PhoneNumber;

            var updatedUser = await _userManager.UpdateAsync(user);
            if (!updatedUser.Succeeded) return BadRequest(updatedUser.Errors);

            return Ok(new { message = "User updated successfully" });

        }


        //Delete user by Id  /api/auth
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            var appt = await _appointmentRepository.GetUserAppointments(user.Id);
            if(appt != null)
            {
                foreach (var item in appt)
                {
                    await _appointmentRepository.DeleteAppointment(item.Id);
                }
            }
            
            //     var user = _dbContext.Users
            //.Include(u => u.Appointments)
            //.Include(u => u.Roles) // if applicable
            //.FirstOrDefault(u => u.Id == id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "User deleted successfully" });
        }
    }
}

