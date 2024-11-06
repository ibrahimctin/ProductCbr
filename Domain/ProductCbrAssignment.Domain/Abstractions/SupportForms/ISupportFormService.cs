using ProductCbrAssignment.Domain.DTOs.SupportForms;

namespace ProductCbrAssignment.Domain.Abstractions.SupportForms
{
    public interface ISupportFormService
    {
        Task<bool> CreateSupportForm(CreateSupportFormRequest createSupportFormRequest);
        Task<bool> RemoveSupportForm(string id);    
        Task<List<SupportFormResponse>> GetSupportForms();
    }
}
