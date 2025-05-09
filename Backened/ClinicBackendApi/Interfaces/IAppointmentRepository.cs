using ClinicBackendApi.Models;

namespace ClinicBackendApi.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointments();

        Task<Appointment> GetAppointmentById(int id);

        Task<IEnumerable<Appointment>> GetUserAppointments(string id);

        Task<Appointment> AddAppointment(Appointment appointment);

        Task<bool> UpdateAppointment(Appointment appointment);

        Task<bool> DeleteAppointment(int id);
    }
}