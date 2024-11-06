using ProductCbrAssignment.Domain.Entities.SupportForms;
using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;

namespace ProductCbrAssignment.Infrastructure.Repositories.Implementations
{
    public class SupportFormRepository : Repository<SupportForm>, ISupportFormRepository
    {
        public SupportFormRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}