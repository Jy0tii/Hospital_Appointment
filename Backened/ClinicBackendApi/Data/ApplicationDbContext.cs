using ClinicBackendApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicBackendApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Appointment>()
        //        .HasOne(a => a.User)
        //        .WithMany(u => u.Appointments)
        //        .HasForeignKey(a => a.UserId)
        //        .OnDelete(DeleteBehavior.Cascade); // 👈 enables cascade delete
        //}

        //prevent deletion 

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Appointment>()
        //        .HasOne(a => a.User)
        //        .WithMany(u => u.Appointments)
        //        .HasForeignKey(a => a.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}

        //if (user.Appointments.Any())
        //{   
        //    return BadRequest("Cannot delete user with existing appointments.");
        //}
}
}

