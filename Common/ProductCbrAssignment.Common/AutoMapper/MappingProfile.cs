using AutoMapper;
using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.DTOs.Products;
using ProductCbrAssignment.Domain.DTOs.SupportForms;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Domain.Entities.Products;
using ProductCbrAssignment.Domain.Entities.SupportForms;

namespace ProductCbrAssignment.Common.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserResponse>().ReverseMap();
            CreateMap<ProductCreateRequest, Product>().ReverseMap();
            CreateMap<ProductUpdateRequest, Product>().ReverseMap();
            CreateMap<ProductDetailResponse, Product>().ReverseMap();
            CreateMap<CreateSupportFormRequest, SupportForm>().ReverseMap();
            CreateMap<SupportFormResponse, SupportForm>().ReverseMap();
            CreateMap<SupportForm, SupportFormResponse>()
            .ForMember(dest => dest.UserResponse, opt => opt.MapFrom(src => src.User));
        }
    }
}
