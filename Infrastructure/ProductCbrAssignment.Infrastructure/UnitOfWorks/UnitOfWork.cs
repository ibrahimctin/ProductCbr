using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;
using ProductCbrAssignment.Infrastructure.Repositories.Implementations;

namespace ProductCbrAssignment.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<ISupportFormRepository> _supportRepository;
        private readonly Lazy<IProductRepository> _productRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _supportRepository = new Lazy<ISupportFormRepository>(() => new SupportFormRepository(context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context)); 
        }

        public ISupportFormRepository SupportForm => _supportRepository.Value;
        public IProductRepository Product => _productRepository.Value;

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}