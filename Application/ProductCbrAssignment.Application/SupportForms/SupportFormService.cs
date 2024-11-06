using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Hosting;
using ProductCbrAssignment.Common.ExceptionModels;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Abstractions.SupportForms;
using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.DTOs.SupportForms;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Domain.Entities.SupportForms;
using ProductCbrAssignment.Infrastructure.UnitOfWorks;

namespace ProductCbrAssignment.Application.SupportForms
{
    public class SupportFormService:ISupportFormService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uOw;
        private readonly ICurrentUserService _currentUserService;

        public SupportFormService(
            IMapper mapper,
            IUnitOfWork uOw,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _uOw = uOw;
            _currentUserService = currentUserService;
        }

        public async Task<bool> CreateSupportForm(CreateSupportFormRequest createSupportFormRequest)
        {
            var supportFormPayload = MapToDbSupportFormCreateModel(createSupportFormRequest);
            var payloaduser = await CurrentUser();
            supportFormPayload.User = payloaduser;
            supportFormPayload.CreatedDate = createSupportFormRequest.CreatedDate = DateTime.UtcNow;
            supportFormPayload.Status = SupportFormStatus.Pending;
        
            _mapper.Map<AppUserResponse>(supportFormPayload.User);

            if (supportFormPayload is not null)
            {
                await _uOw.SupportForm.AddAsync(supportFormPayload);
                await _uOw.Save();
            }
            return supportFormPayload is null ? false : true;
        }


        public async Task<List<SupportFormResponse>> GetSupportForms()
        {
            var supportFormListResponse = await _uOw.SupportForm.GetAllAsync();
            var supportFormPayload = _mapper.Map<List<SupportFormResponse>>(supportFormListResponse);
            return supportFormPayload;
        }

        public async Task<bool> RemoveSupportForm(string id)
        {
           
            var supportFormResult = await GetSupportFormIfExists(id);

            supportFormResult.Status = SupportFormStatus.Deleted;

            await _uOw.SupportForm.DeleteAsync(supportFormResult);

            await _uOw.Save();

            return true;
        }

        private async Task<SupportForm> GetSupportFormIfExists(string id)
        {
            var result = await _uOw.SupportForm.GetById(id);
          
            if (result == null)
                throw new SupportFormNotFoundException(result.Id);

            return result;
        }
        private SupportForm MapToDbSupportFormCreateModel(CreateSupportFormRequest request) => (_mapper.Map<SupportForm>(request));
        private async Task<AppUser> CurrentUser() => await _currentUserService.GetCurrentUser();
    }
}
