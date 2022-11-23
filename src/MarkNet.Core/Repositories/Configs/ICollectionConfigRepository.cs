using MarkNet.Core.Repositories.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkNet.Core.Repositories.Configs
{
    public interface ICollectionConfigRepository<T> : IRepository
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> configs);
        Task<IEnumerable<T>> RemoveAllAsync(IEnumerable<T> entities);
    }
}
