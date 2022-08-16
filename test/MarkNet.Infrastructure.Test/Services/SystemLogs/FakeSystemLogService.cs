using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Services.SystemLogs;
using MarkNet.Test.Contexts;
using MarkNet.Test.Entities;

namespace MarkNet.Test.Services.SystemLogs
{
    internal class FakeSystemLogService : SystemLogService<FakeSystemLogEntity, TestContext>
    {
        public FakeSystemLogService(IMergedRepository<TestContext> mergedRepository) : base(mergedRepository)
        {
        }
    }
}
