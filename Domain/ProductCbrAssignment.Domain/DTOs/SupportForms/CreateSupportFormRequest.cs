using ProductCbrAssignment.Domain.Entities.SupportForms;

namespace ProductCbrAssignment.Domain.DTOs.SupportForms
{
    public class CreateSupportFormRequest
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; } 
        public SupportFormStatus Status { get; set; }
    }
}
