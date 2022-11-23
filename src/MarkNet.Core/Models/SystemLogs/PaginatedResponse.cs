using System.Collections.Generic;

namespace MarkNet.Core.Models.SystemLogs
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponseMetadata Metadata { get; set; } = null!;
        public IEnumerable<T> Records { get; set; } = null!;
    }

    public class PaginatedResponseMetadata
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
