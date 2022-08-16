using MarkNet.Core.Repositories.Commons;

namespace MarkNet.Core.Repositories.Configs
{
    public interface ICollectionConfigRepository<T> : IRepository
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> configs);
        Task<IEnumerable<T>> RemoveAllAsync(IEnumerable<T> entities);
    }
}
