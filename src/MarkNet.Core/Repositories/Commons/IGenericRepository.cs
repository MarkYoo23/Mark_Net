using MarkNet.Core.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkNet.Core.Repositories.Commons
{
    public interface IGenericRepository<T> : IRepository
        where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
