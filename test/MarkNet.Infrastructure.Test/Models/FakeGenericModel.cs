using MarkNet.Core.Models;

namespace MarkNet.Test.Models
{
    public class FakeGenericModel : PropertyModel<FakeGenericModel>
    {
        public int? Value { get; set; }
    }
}
