using MarkNet.Core.Entities.Configs;
using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Configs
{
    public abstract class CollectionConfigService<TModel, TEntity>
        where TModel : PropertyModel<TModel>, new()
        where TEntity : TModel, ICollectionConfigEntity, new()
    {
        private readonly CollectionCashManager<TModel> _cashManager;
        private readonly IMergedRepository _mergedRepository;

        public CollectionConfigService(
            CollectionCashManager<TModel> cashManager,
            IMergedRepository mergedRepository)
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

            await _cashManager.SetAsync(values);
        }

        public Task<IEnumerable<TModel>> GetAsync()
        {
            var values = _cashManager.Get();
            return Task.FromResult(values);
        }

        public async Task SetAsync(IEnumerable<TModel> values)
        {
            var repository = _mergedRepository.GetRepository<ICollectionConfigRepository<TEntity>>();

            var beforeEntities = await repository.GetAllAsync();
            await repository.RemoveAllAsync(beforeEntities);
            await _mergedRepository.SaveChangeAsync();

            var number = 0;
            var entities = values.Select(row =>
            {
                var entity = new TEntity();
                entity.CopyValues(row);
                entity.Id = 0;
                entity.Number = ++number;
                return entity;
            });
            await repository.AddRangeAsync(entities);
            await _mergedRepository.SaveChangeAsync();

            await _cashManager.SetAsync(values);
        }
    }
}
