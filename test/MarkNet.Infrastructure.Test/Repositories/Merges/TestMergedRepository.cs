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
            var fakeOneRepository = new GenericRepository<FakeOneEntity>(context.FakeOnes);
            RegisterRepository(typeof(IGenericRepository<FakeOneEntity>), fakeOneRepository);

            var fakeTwoRepository = new GenericRepository<FakeTwoEntity>(context.FakeTwos);
            RegisterRepository(typeof(IGenericRepository<FakeTwoEntity>), fakeTwoRepository);

            var fakeCollectionConfigRepository = new CollectionConfigRepository<FakeCollectionConfigEntity>(context.FakeCollectionConfigs);
            RegisterRepository(typeof(ICollectionConfigRepository<FakeCollectionConfigEntity>), fakeCollectionConfigRepository);

            var fakeConfigRepository = new ConfigRepository<FakeConfigEntity>(context.FakeConfigs);
            RegisterRepository(typeof(IConfigRepository<FakeConfigEntity>), fakeConfigRepository);

            var fakeSystemLogRepository = new SystemLogRepository<FakeSystemLogEntity>(context.FakeSystemLogs);
            RegisterRepository(typeof(ISystemLogRepository<FakeSystemLogEntity>), fakeSystemLogRepository);
        }
    }
}
