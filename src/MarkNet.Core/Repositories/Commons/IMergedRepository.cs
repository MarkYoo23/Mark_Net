using Microsoft.EntityFrameworkCore;

namespace MarkNet.Core.Repositories.Commons
{
    public interface IMergedRepository<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        T GetRepository<T>() where T : class;
    }
}
