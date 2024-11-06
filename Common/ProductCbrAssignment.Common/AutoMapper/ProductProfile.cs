using AutoMapper;
using ProductCbrAssignment.Domain.DTOs.Products;
using ProductCbrAssignment.Domain.Entities.Products;

namespace ProductCbrAssignment.Common.AutoMapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, Product>().ReverseMap();
            CreateMap<ProductUpdateRequest, Product>().ReverseMap();
            CreateMap<ProductDetailResponse, Product>().ReverseMap();
        }
    }
}