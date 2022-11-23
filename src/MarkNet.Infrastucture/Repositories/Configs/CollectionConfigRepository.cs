using MarkNet.Core.Entities.Configs;
using MarkNet.Core.Repositories.Configs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkNet.Infrastructure.Repositories.Configs
{
    public class CollectionConfigRepository<TEntity> : ICollectionConfigRepository<TEntity>
        where TEntity : class, ICollectionConfigEntity
    {
        private readonly DbSet<TEntity> _entities;

        public CollectionConfigRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> configs)
        {
            await _entities.AddRangeAsync(configs);
            return configs;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities
                .OrderBy(col => col.Number)
                .ToArrayAsync();
        }

        public Task<IEnumerable<TEntity>> RemoveAllAsync(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            return Task.FromResult(entities);
        }
    }
}
