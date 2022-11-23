using System.Threading.Tasks;

namespace MarkNet.Core.Repositories.Commons
{
    public interface IUnitOfWork
    {
        // TODO : (dh) Add Transaction.
        Task<bool> SaveEntitiesAsync();
        Task<int> SaveChangeAsync();
    }
}
