using Microsoft.EntityFrameworkCore.Storage;

namespace MarkNet.Core.Repositories.Commons
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> CreateTransactionAsync();
        Task<bool> SaveEntitiesAsync();
        Task<int> SaveChangeAsync();
    }
}
