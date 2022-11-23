using MarkNet.Core.Entities.Commons;
using System;

namespace MarkNet.Core.Entities.SystemLogs
{
    public interface ISystemLogEntity : IEntity
    {
        DateTime Created { get; set; }
    }
}
