using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Repositories.Configs;
using MarkNet.Core.Services.Cashings;
using System.Threading.Tasks;

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

            await _cashManager.SetAsync(model);
        }

        public async Task<TModel> GetAsync() => await _cashManager.GetAsync();
        public TModel Get() => _cashManager.Get();

        public async Task SetAsync(TModel values)
        {
            // Need Locking
            var repository = _mergedRepository.GetRepository<IConfigRepository<TEntity>>();

            var entity = await repository.GetAsync();
            entity.CopyValues(values);

            await repository.SetAsync(entity);
            await _mergedRepository.SaveChangeAsync();

            var model = new TModel();
            model.CopyValues(entity);

            await _cashManager.SetAsync(model);
        }

        public async Task PatchAsync(TModel values)
        {
            var repository = _mergedRepository.GetRepository<IConfigRepository<TEntity>>();

            var entity = await repository.GetAsync();
            entity.PatchValues(values);

            await repository.SetAsync(entity);
            await _mergedRepository.SaveChangeAsync();

            var model = new TModel();
            model.CopyValues(entity);

            await _cashManager.SetAsync(model);
        }
    }
}
