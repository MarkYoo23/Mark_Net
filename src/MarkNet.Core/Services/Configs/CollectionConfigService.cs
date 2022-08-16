using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;
using Microsoft.EntityFrameworkCore;

namespace MarkNet.Core.Services.Configs
{
    public abstract class CollectionConfigService<TModel, TEntity, TContext>
        where TModel : PropertyModel<TModel>, new()
        where TEntity : TModel, new()
        where TContext : DbContext
    {
        private readonly CollectionCashManager<TModel> _cashManager;
        private readonly IMergedRepository<TContext> _mergedRepository;

        public CollectionConfigService(
            CollectionCashManager<TModel> cashManager,
            IMergedRepository<TContext> mergedRepository)
        {
            _cashManager = cashManager;
            _mergedRepository = mergedRepository;
        }

        public async Task InitializeAsync()
        {
            var repository = _mergedRepository.GetRepository<ICollectionConfigRepository<TEntity>>();

            var values = (await repository.GetAllAsync())
                .Select(row =>
                {
                    var model = new TModel();
                    model.CopyValues(row);
                    return model;
                });

            _cashManager.Set(values);
        }

        public Task<IEnumerable<TModel>> GetAsync()
        {
            var values = _cashManager.Get();
            return Task.FromResult(values);
        }

        public async Task SetAsync(IEnumerable<TModel> values)
        {
            var entities = values.Select(row =>
            {
                var entity = new TEntity();
                entity.CopyValues(row);
                return entity;
            });

            var repository = _mergedRepository.GetRepository<ICollectionConfigRepository<TEntity>>();

            await repository.AddRangeAsync(entities);
            await _mergedRepository.SaveChangeAsync();

            _cashManager.Set(values);
        }
    }
}
