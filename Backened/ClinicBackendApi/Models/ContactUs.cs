using System.ComponentModel.DataAnnotations;

namespace ClinicBackendApi.Models
{
    public class ContactUs
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set;  }
    }
}
