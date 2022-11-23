using MarkNet.Core.Entities.Configs;
using MarkNet.Core.Repositories.Configs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MarkNet.Infrastructure.Repositories.Configs
{
    public class ConfigRepository<TEntity> : IConfigRepository<TEntity>
        where TEntity : class, IConfigEntity
    {
        private readonly DbSet<TEntity> _entities;

        public ConfigRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }

        public async Task<TEntity> GetAsync()
        {
            return await _entities.FirstAsync();
        }

        public Task<TEntity> SetAsync(TEntity config)
        {
            var entity = _entities.Update(config);
            return Task.FromResult(entity.Entity);
        }
    }
}
