using MarkNet.Core.Repositories.Commons;
using System.Threading.Tasks;

namespace MarkNet.Core.Repositories.Configs
{
    public interface IConfigRepository<T> : IRepository
    {
        Task<T> GetAsync();
        Task<T> SetAsync(T config);
    }
}
