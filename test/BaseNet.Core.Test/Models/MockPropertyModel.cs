using MarkNet.Core.Models;

namespace BaseNet.Core.Test.Models
{
    public class MockPropertyModel : PropertyModel<MockPropertyModel>
    {
        public int NormalInt { get; set; }
        public int? NullableInt { get; set; }
        public string NormalString { get; set; } = null!;
        public string? NullableString { get; set; }
        public DateTime NormalTime { get; set; }
        public DateTime? NullableTime { get; set; }
    }
}
