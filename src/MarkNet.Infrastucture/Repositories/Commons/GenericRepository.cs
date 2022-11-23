using MarkNet.Core.Entities.Commons;
using MarkNet.Core.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkNet.Infrastructure.Repositories.Commons
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _entities;

        public GenericRepository(DbSet<T> entities)
        {
            _entities = entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _entities.FirstAsync(row => row.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _entities.RemoveRange(entity);
        }

        public string GetName()
        {
            var name = typeof(T).FullName!;
            return name;
        }
    }
}
