using MarkNet.Core.Entities.Commons;

namespace MarkNet.Core.Entities.SystemLogs
{
    public interface ISystemLogEntity : IEntity
    {
        DateTime Created { get; set; }
    }
}
