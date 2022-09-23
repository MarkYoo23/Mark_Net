using MarkNet.Core.Services.Cashings;
using MarkNet.Core.Services.Configs;
using MarkNet.Test.Entities;
using MarkNet.Test.Models;
using MarkNet.Test.Repositories.Merges;

namespace MarkNet.Test.Services.Configs
{
    internal class FakeConfigService : ConfigService<FakeConfig, FakeConfigEntity>
    {
        public FakeConfigService(
            CashManager<FakeConfig> cashManager,
            ITestMergedRepository mergedRepository) 
            : base(cashManager, mergedRepository)
        {
        }
    }
}
