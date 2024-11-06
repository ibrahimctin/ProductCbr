using AutoMapper;
using ProductCbrAssignment.Common.ExceptionModels;
using ProductCbrAssignment.Domain.Abstractions.Products;
using ProductCbrAssignment.Domain.DTOs.Products;
using ProductCbrAssignment.Domain.Entities.Products;
using ProductCbrAssignment.Infrastructure.UnitOfWorks;

namespace ProductCbrAssignment.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uOw;

        public ProductService(
            IMapper mapper, 
            IUnitOfWork uOw)
        {
            _mapper = mapper;
            _uOw = uOw;
        }

        public async Task<bool> CreateProductAsync(ProductCreateRequest request)
        {
            var productPayload = _mapper.Map<Product>(request);
         
            productPayload.CreationDate = request.CreationDate = DateTime.UtcNow;
          
            if (productPayload is not null)
            {
                await _uOw.Product.AddAsync(productPayload);
                await _uOw.Save();
            }
            return productPayload is null ? false : true;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var productPayload = await GetProduct(id);

            await _uOw.Product.DeleteAsync(productPayload);

            var savedProduct = _uOw.Save();

            if (savedProduct != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProductDetailResponse>> GetAllProductsAsync()
        {
            var productListPayload = await _uOw.Product.GetAllAsync();
            return _mapper.Map<List<ProductDetailResponse>>(productListPayload);
        }

        public async Task<ProductDetailResponse> GetProductDetail(string id)
        {
            var productFromDb = await GetProductDetailAsync(id);
            return _mapper.Map<ProductDetailResponse>(productFromDb);
        }

        public async Task<bool> UpdateProductRequest(ProductUpdateRequest request)
        {
            var productFromDb =  GetProductDetail(request.Id);
            var productPayload = MapToDbProductUpdateModel(request);
            if (productFromDb is null) throw new ProductNotFoundException(request.Id);
            if (productFromDb is not null)
            {
               await _uOw.Product.Update(productPayload);
               await _uOw.Save();
            }
            return productPayload is null ? false : true;
        }

        private async Task<Product> GetProductDetailAsync(string id)
        {
            var result = await GetProduct(id);

            if (result == null)
                throw new ProductNotFoundException(result.Id);
            return result;
        }


        private async Task<Product> GetProduct(string id) => await _uOw.Product.GetById(id);
        private Product MapToDbProductCreateModel(ProductCreateRequest createRequest) => (_mapper.Map<Product>(createRequest));
        private Product MapToDbProductUpdateModel(ProductUpdateRequest updateRequest) => (_mapper.Map<Product>(updateRequest));
        private Product MapToDbModelForRemove(Product product) => (_mapper.Map<Product>(product));
        private ProductDetailResponse MapToDbProductUpdateModel(Product product) => (_mapper.Map<ProductDetailResponse>(product));

    }
}
