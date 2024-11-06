using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Domain.Entities.SupportForms
{
    public class SupportForm:BaseEntity
    {
        [Required]
        public string UserId { get; set; } 

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; } 

        [Required]
        [StringLength(250)]
        public string Subject { get; set; } 

        [Required]
        public string Message { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now; 

        [Required]
        public SupportFormStatus Status { get; set; } = SupportFormStatus.Pending;
    }
}
