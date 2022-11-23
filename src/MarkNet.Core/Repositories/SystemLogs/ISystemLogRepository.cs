using MarkNet.Core.Models.SystemLogs;
using MarkNet.Core.Repositories.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkNet.Core.Repositories.SystemLogs
{
    public interface ISystemLogRepository<T> : IRepository
    {
        Task<T> AddAsync(T log);
        Task AddRangeAsync(IEnumerable<T> logs);
        Task<IEnumerable<T>> GetLogsAsync(DatePagedParameter parameter);
        Task<IEnumerable<T>> GetLogsAsync(DateRangedParameter parameter);
        Task<int> GetCountAsync(DateRangedParameter parameter);
        Task<IEnumerable<T>> GetLastAsync(int count);
    }
}
