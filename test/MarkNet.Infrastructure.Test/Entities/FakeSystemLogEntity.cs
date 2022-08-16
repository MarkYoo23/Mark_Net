using MarkNet.Core.Entities.SystemLogs;

namespace MarkNet.Test.Entities
{
    public class FakeSystemLogEntity : ISystemLogEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Value { get; set; }
    }
}
