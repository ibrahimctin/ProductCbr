using AutoMapper;
using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Common.AutoMapper
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser,AppUserResponse>().ReverseMap();
            CreateMap<AppUser, RegisterRequest>().ReverseMap();
        }
    }
}