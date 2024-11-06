using ProductCbrAssignment.Domain.Entities.Products;
using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;

namespace ProductCbrAssignment.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
