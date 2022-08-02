namespace MarkNet.Core.Repositories.Commons
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
