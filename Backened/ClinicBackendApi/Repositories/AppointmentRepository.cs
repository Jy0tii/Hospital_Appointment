using ClinicBackendApi.Data;
using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicBackendApi.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _context.Appointment.ToListAsync();
        }
        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _context.Appointment.FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetUserAppointments(string userId)
        {
            return await _context.Appointment.Where(a => a.UserId == userId).ToListAsync(); 
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            _context.Appointment.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointment.Remove(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
