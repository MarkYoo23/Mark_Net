using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Services.Cashings;
using MarkNet.Core.Services.Configs;
using MarkNet.Test.Contexts;
using MarkNet.Test.Entities;
using MarkNet.Test.Models;

namespace MarkNet.Test.Services.Configs
{
    public class FakeCollectionConfigService : CollectionConfigService<FakeCollectionConfig, FakeCollectionConfigEntity, TestContext>
    {
        public FakeCollectionConfigService(
            CollectionCashManager<FakeCollectionConfig> cashManager,
            IMergedRepository<TestContext> mergedRepository)
            : base(cashManager, mergedRepository)
        {
        }
    }
}
