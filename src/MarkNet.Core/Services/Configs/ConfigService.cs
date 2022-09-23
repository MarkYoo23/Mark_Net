using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;

namespace MarkNet.Core.Services.Configs
{
    public abstract class ConfigService<TModel, TEntity>
        where TModel : PropertyModel<TModel>, new()
        where TEntity : TModel, new()
    {
        private readonly CashManager<TModel> _cashManager;
        private readonly IMergedRepository _mergedRepository;

        public ConfigService(
            CashManager<TModel> cashManager,
            IMergedRepository mergedRepository)
        {
            _cashManager = cashManager;
            _mergedRepository = mergedRepository;
        }

        public async Task InitializeAsync()
        {
            var repository = _mergedRepository.GetRepository<IConfigRepository<TEntity>>();

            var entity = await repository.GetAsync();
            var model = new TModel();
            model.CopyValues(entity);
            _cashManager.Set(model);
        }

        public Task<TModel> GetAsync()
        {
            var values = _cashManager.Get();
            return Task.FromResult(values);
        }

        public async Task SetAsync(TModel values)
        {
            var entity = new TEntity();
            entity.CopyValues(values);

            var repository = _mergedRepository.GetRepository<IConfigRepository<TEntity>>();

            await repository.SetAsync(entity);
            await _mergedRepository.SaveChangeAsync();

            _cashManager.Set(values);
        }
    }
}
