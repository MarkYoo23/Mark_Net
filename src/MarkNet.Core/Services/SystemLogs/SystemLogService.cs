using MarkNet.Core.Entities.SystemLogs;
using MarkNet.Core.Models.SystemLogs;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.SystemLogs;

namespace MarkNet.Core.Services.SystemLogs
{
    public abstract class SystemLogService<T> where T : ISystemLogEntity
    {
        private readonly ISystemLogRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LogMapper _mapper = new LogMapper();

        public SystemLogService(
            ISystemLogRepository<T> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> PutAsync(T entity) 
        {
            await _repository.AddAsync(entity);
            return await _unitOfWork.SaveEntitiesAsync();
        }

        public async Task<PaginatedResponse<T>> GetPagedAsync(DatePagedParameter parameter)
        {
            var dataCount = await _repository.GetCountAsync(parameter);
            var pageCount = (int)Math.Ceiling(dataCount / (double)parameter.Limit);
            var records = await _repository.GetLogsAsync(parameter);
            var response = _mapper.MapLogs(records, parameter.Offset, parameter.Limit, pageCount);
            return response;
        }

        public async Task<IEnumerable<T>> GetRangeAsync(DateRangedParameter parameter)
        {
            var records = await _repository.GetLogsAsync(parameter);
            return records;
        }
    }
}
