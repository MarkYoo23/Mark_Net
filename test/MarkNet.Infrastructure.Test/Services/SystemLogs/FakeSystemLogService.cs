using MarkNet.Core.Services.SystemLogs;
using MarkNet.Test.Entities;
using MarkNet.Test.Repositories.Merges;

namespace MarkNet.Test.Services.SystemLogs
{
    internal class FakeSystemLogService : SystemLogService<FakeSystemLogEntity>
    {
        public FakeSystemLogService(ITestMergedRepository mergedRepository) : base(mergedRepository)
        {
        }
    }
}
