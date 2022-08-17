using MarkNet.Core.Services.Commons;
using MarkNet.Test.Entities;
using MarkNet.Test.Models;
using MarkNet.Test.Repositories.Merges;

namespace MarkNet.Test.Services.Commons
{
    internal class FakeGenericService : GenericService<FakeGenericModel, FakeGenericEntity>
    {
        public FakeGenericService(ITestMergedRepository mergedRepository) : base(mergedRepository)
        {
        }
    }
}
