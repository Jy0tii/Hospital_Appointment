using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBackendApi.Models
{
    public class Appointment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; } 

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$")]
        public string MobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public bool IsConfirmed { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
