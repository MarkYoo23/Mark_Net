using MarkNet.Core.Repositories.Commons;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MarkNet.Infrastructure.Repositories.Commons
{
    public class UnitOfWork<TContext> : IUnitOfWork
         where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<int> SaveChangeAsync()
        {
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows;
        }
    }
}
