using ClinicBackendApi.Data;
using ClinicBackendApi.Interfaces;
using ClinicBackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicBackendApi.Repositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactUs>> GetContactUs()
        {
            return await _context.ContactUs.ToListAsync();
        }

        public async Task<ContactUs> AddContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Add(contactUs);
            await _context.SaveChangesAsync();
            return contactUs;
        }

        public async Task<bool> DeleteContactUs(int id)
        {
            var details = await _context.ContactUs.FindAsync(id);
            if (details == null) return false;

            _context.ContactUs.Remove(details);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
