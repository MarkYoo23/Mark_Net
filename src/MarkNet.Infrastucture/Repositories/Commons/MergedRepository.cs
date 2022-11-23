using MarkNet.Core.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MarkNet.Infrastructure.Repositories.Commons
{
    public class MergedRepository<TContext> : UnitOfWork<TContext>, IMergedRepository
         where TContext : DbContext
    {
        private Dictionary<string, IRepository> _repositories;

        public MergedRepository(TContext context) : base(context)
        {
            _repositories = new Dictionary<string, IRepository>();
        }

        protected void RegisterRepository(Type type, IRepository repository)
        {
            var typeName = type.FullName!;
            _repositories.Add(typeName, repository);
        }

        public T GetRepository<T>() where T : class
        {
            var typeName = typeof(T).FullName!;

            if (_repositories.TryGetValue(typeName, out var repository))
            {
                return (T)repository;
            }

            throw new NotImplementedException();
        }
    }
}
