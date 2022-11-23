using MarkNet.Core.Models.SystemLogs;
using System.Collections.Generic;

namespace MarkNet.Core.Services.SystemLogs
{
    public class LogMapper
    {
        public PaginatedResponse<T> MapLogs<T>(IEnumerable<T> logs, int offset, int limit, int total)
        {
            return new PaginatedResponse<T>()
            {
                Metadata = new PaginatedResponseMetadata()
                {
                    Offset = offset,
                    Limit = limit,
                    Total = total,
                },
                Records = logs
            };
        }
    }
}
