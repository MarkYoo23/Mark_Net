using MarkNet.Core.Entities.Configs;
using MarkNet.Test.Models;

namespace MarkNet.Test.Entities
{
    public class FakeConfigEntity : FakeConfig, IConfigEntity
    {
        public int Id { get; set; }
    }
}
