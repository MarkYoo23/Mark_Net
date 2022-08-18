using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Repositories.SystemLogs;
using MarkNet.Infrastructure.Repositories.Commons;
using MarkNet.Infrastructure.Repositories.Configs;
using MarkNet.Infrastructure.Repositories.SystemLogs;
using MarkNet.Test.Contexts;
using MarkNet.Test.Entities;

namespace MarkNet.Test.Repositories.Merges
{
    public interface ITestMergedRepository : IMergedRepository
    {
    }

    public class TestMergedRepository : MergedRepository<TestContext>, ITestMergedRepository
    {
        public TestMergedRepository(TestContext context) : base(context)
        {
            RegisterRepository(
                typeof(IGenericRepository<FakeOneEntity>),
                new GenericRepository<FakeOneEntity>(context.FakeOnes));

            RegisterRepository(
                typeof(IGenericRepository<FakeTwoEntity>),
                new GenericRepository<FakeTwoEntity>(context.FakeTwos));

            RegisterRepository(
                typeof(ICollectionConfigRepository<FakeCollectionConfigEntity>),
                new CollectionConfigRepository<FakeCollectionConfigEntity>(context.FakeCollectionConfigs));

            RegisterRepository(
                typeof(IConfigRepository<FakeConfigEntity>),
                new ConfigRepository<FakeConfigEntity>(context.FakeConfigs));

            RegisterRepository(
                typeof(ISystemLogRepository<FakeSystemLogEntity>),
                new SystemLogRepository<FakeSystemLogEntity>(context.FakeSystemLogs));

            RegisterRepository(
                typeof(IGenericRepository<FakeGenericEntity>),
                new GenericRepository<FakeGenericEntity>(context.FakeGenerics));
        }
    }
}
