using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ClinicBackendApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$")]
        public string PinCode { get; set; }

        public string? MedicalHistory { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }  //navigation property

        //[Required]
        //[RegularExpression(@"^\d{10}$")]
        //public string PhoneNumber { get; set; }

        //[DataType(DataType.EmailAddress)]
        //public string? Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[NotMapped]
        //public string ConfirmPassword { get; set; }
    }
}
