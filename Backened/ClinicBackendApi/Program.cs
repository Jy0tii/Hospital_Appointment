using System;
using System.Text;
using ClinicBackendApi.Data;
using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using ClinicBackendApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
      
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Repository registration (by me)
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();

//Database connect service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = true;
        //options.Password.RequireUppercase = true;
        //options.Password.RequireLowercase = true;
        //options.Password.RequiredLength = 8;
        //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Api call in angular
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular runs on 4200 by default
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//Add JWT authentication 

builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    });

//disable login redirects (which cause /Account/Login)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

// Add Authorization
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//for dbseeder or role adding and default admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesUserAsync(services);
}

app.UseHttpsRedirection();
app.UseCors(); // Add Api call in angular
app.UseAuthentication();
app.UseAuthorization();
// Log out the token and the authentication status
builder.Services.AddLogging(options =>
{
    options.AddConsole();
});
app.MapControllers();

app.Run();




// Inside Program.cs or  Helpers/DbSeeder.cs (or Services/DbSeeder.cs) & for default admin

static async Task SeedRolesUserAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123"; // Make sure password meets Identity password policy

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Admin",
            Gender = "Female",
            Address = "Delhi",
            City= "Delhi",
            State = "Delhi",
            PinCode = "123456",
           DateOfBirth = DateTime.Parse("2025-05-08T14:30"),
           PhoneNumber = "1234567895",


            //EmailConfirmed = true // optional: depends if you want to skip email confirmation
        };

        var createAdminResult = await userManager.CreateAsync(newAdmin, adminPassword);
        if (createAdminResult.Succeeded)
        {
            // Step 3: Assign "Admin" role
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}


// move users data in appsetting.json 
//"SeedUsers": [
//  {
//    "Email": "admin@example.com",
//    "Password": "Admin@123",
//    "Role": "Admin"
//  },