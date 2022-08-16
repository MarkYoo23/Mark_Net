using MarkNet.Core.Entities.SystemLogs;
using MarkNet.Core.Models.SystemLogs;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.SystemLogs;
using Microsoft.EntityFrameworkCore;

namespace MarkNet.Core.Services.SystemLogs
{
    public abstract class SystemLogService<TEntity, TContext> 
        where TEntity : ISystemLogEntity
        where TContext : DbContext
    {
        private readonly LogMapper _mapper = new LogMapper();
        private readonly IMergedRepository<TContext> _mergedRepository;

        public SystemLogService(IMergedRepository<TContext> mergedRepository)
        {
            _mergedRepository = mergedRepository;
        }

        public async Task<bool> PutAsync(TEntity entity)
        {
            var repository = _mergedRepository.GetRepository<ISystemLogRepository<TEntity>>();

            await repository.AddAsync(entity);
            return await _mergedRepository.SaveEntitiesAsync();
        }

        public async Task<PaginatedResponse<TEntity>> GetPagedAsync(DatePagedParameter parameter)
        {
            var repository = _mergedRepository.GetRepository<ISystemLogRepository<TEntity>>();

            var dataCount = await repository.GetCountAsync(parameter);
            var pageCount = (int)Math.Ceiling(dataCount / (double)parameter.Limit);
            var records = await repository.GetLogsAsync(parameter);
            var response = _mapper.MapLogs(records, parameter.Offset, parameter.Limit, pageCount);
            return response;
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(DateRangedParameter parameter)
        {
            var repository = _mergedRepository.GetRepository<ISystemLogRepository<TEntity>>();

            var records = await repository.GetLogsAsync(parameter);
            return records;
        }
    }
}
