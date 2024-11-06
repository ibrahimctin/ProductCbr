using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;

namespace ProductCbrAssignment.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ISupportFormRepository SupportForm { get; }
        IRepository<T> GetRepository<T>() where T : class, new();
        Task Save();
    }
}
