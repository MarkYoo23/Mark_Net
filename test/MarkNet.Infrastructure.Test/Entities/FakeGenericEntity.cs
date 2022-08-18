using MarkNet.Core.Entities.Commons;
using MarkNet.Test.Models;

namespace MarkNet.Test.Entities
{
    public class FakeGenericEntity : FakeGenericModel, IEntity
    {
        public int Id { get; set; }
    }
}
