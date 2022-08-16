using MarkNet.Core.Repositories.Commons;

namespace MarkNet.Core.Repositories.Configs
{
    public interface IConfigRepository<T> : IRepository
    {
        Task<T> GetAsync();
        Task<T> SetAsync(T config);
    }
}
