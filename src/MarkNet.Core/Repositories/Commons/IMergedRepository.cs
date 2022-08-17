namespace MarkNet.Core.Repositories.Commons
{
    public interface IMergedRepository : IUnitOfWork
    {
        T GetRepository<T>() where T : class;
    }
}
