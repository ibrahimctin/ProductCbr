using ProductCbrAssignment.Domain.DTOs.Products;

namespace ProductCbrAssignment.Domain.Abstractions.Products
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductCreateRequest request);
        Task<List<ProductDetailResponse>> GetAllProductsAsync();
        Task<bool> DeleteProduct(string id);
        Task<ProductDetailResponse> GetProductDetail(string id);
        Task<bool> UpdateProductRequest(ProductUpdateRequest request);
    }
}
