using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MarkNet.Core.Repositories.Commons
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        Task<IDbContextTransaction> CreateTransactionAsync();
        Task<bool> SaveEntitiesAsync();
        Task<int> SaveChangeAsync();
    }
}
