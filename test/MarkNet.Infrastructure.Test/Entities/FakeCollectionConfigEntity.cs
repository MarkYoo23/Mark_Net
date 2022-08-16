using MarkNet.Core.Entities.Configs;
using MarkNet.Test.Models;

namespace MarkNet.Test.Entities
{
    public class FakeCollectionConfigEntity : FakeCollectionConfig, ICollectionConfigEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }
}
