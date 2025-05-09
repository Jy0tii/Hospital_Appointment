using ClinicBackendApi.Models;

namespace ClinicBackendApi.Interfaces
{
    public interface IContactUsRepository
    {
        Task<IEnumerable<ContactUs>> GetContactUs();

        Task<ContactUs> AddContactUs(ContactUs contactUs);

        Task<bool> DeleteContactUs(int id);
    }
}
