using MarkNet.Core.Entities.SystemLogs;
using MarkNet.Core.Models.SystemLogs;
using MarkNet.Core.Repositories.SystemLogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkNet.Infrastructure.Repositories.SystemLogs
{
    public class SystemLogRepository<TEntity> : ISystemLogRepository<TEntity> where TEntity : class, ISystemLogEntity
    {
        private readonly DbSet<TEntity> _entities;

        public SystemLogRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }

        public async Task<TEntity> AddAsync(TEntity log)
        {
            var entity = await _entities.AddAsync(log);
            return entity.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> logs)
        {
            await _entities.AddRangeAsync(logs);
        }

        public async Task<int> GetCountAsync(DateRangedParameter parameter)
        {
            return await _entities
                .AsNoTracking()
                .Where(log => parameter.From <= log.Created && log.Created <= parameter.To)
                .CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetLogsAsync(DatePagedParameter parameter)
        {
            return await _entities
                .AsNoTracking()
                .Where(log => parameter.From <= log.Created && log.Created <= parameter.To)
                .OrderByDescending(log => log.Created)
                .Skip((parameter.Offset - 1) * parameter.Limit)
                .Take(parameter.Limit)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<TEntity>> GetLogsAsync(DateRangedParameter parameter)
        {
            return await _entities
                .AsNoTracking()
                .Where(log => parameter.From <= log.Created && log.Created <= parameter.To)
                .OrderByDescending(log => log.Created)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<TEntity>> GetLastAsync(int count)
        {
            return await _entities
               .AsNoTracking()
               .OrderByDescending(log => log.Id)
               .Take(count)
               .ToArrayAsync();
        }
    }
}
