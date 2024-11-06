using AutoMapper;
using ProductCbrAssignment.Domain.DTOs.SupportForms;
using ProductCbrAssignment.Domain.Entities.SupportForms;

namespace ProductCbrAssignment.Common.AutoMapper
{
    public class SupportFormProfile:Profile
    {
        public SupportFormProfile()
        {
            CreateMap<CreateSupportFormRequest, SupportForm>().ReverseMap();
            CreateMap<SupportFormResponse,SupportForm>().ReverseMap();
        }
    }
}