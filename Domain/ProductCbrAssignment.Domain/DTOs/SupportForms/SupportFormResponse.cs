using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.Entities.SupportForms;

namespace ProductCbrAssignment.Domain.DTOs.SupportForms
{
    public class SupportFormResponse
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public SupportFormStatus Status { get; set; }
        public string UserName { get; set; }
        public AppUserResponse UserResponse { get; set; }
    }
}
