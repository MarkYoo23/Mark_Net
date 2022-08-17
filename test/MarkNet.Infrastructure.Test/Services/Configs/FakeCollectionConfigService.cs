using MarkNet.Core.Services.Cashings;
using MarkNet.Core.Services.Configs;
using MarkNet.Test.Entities;
using MarkNet.Test.Models;
using MarkNet.Test.Repositories.Merges;

namespace MarkNet.Test.Services.Configs
{
    public class FakeCollectionConfigService : CollectionConfigService<FakeCollectionConfig, FakeCollectionConfigEntity>
    {
        public FakeCollectionConfigService(
            CollectionCashManager<FakeCollectionConfig> cashManager,
            ITestMergedRepository mergedRepository)
            : base(cashManager, mergedRepository)
        {
        }
    }
}
