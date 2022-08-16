using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Services.Cashings;
using MarkNet.Core.Services.Configs;
using MarkNet.Test.Contexts;
using MarkNet.Test.Entities;
using MarkNet.Test.Models;

namespace MarkNet.Test.Services.Configs
{
    internal class FakeConfigService : ConfigService<FakeConfig, FakeConfigEntity, TestContext>
    {
        public FakeConfigService(
            CashManager<FakeConfig> cashManager, 
            IMergedRepository<TestContext> mergedRepository) 
            : base(cashManager, mergedRepository)
        {
        }
    }
}
