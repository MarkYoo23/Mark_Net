using MarkNet.Core.Entities.Commons;
using MarkNet.Core.Models;
using MarkNet.Core.Repositories.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Commons
{
    public class GenericService<TModel, TEntity>
        where TModel : PropertyModel<TModel>, new()
        where TEntity : TModel, IEntity, new()
    {
        private readonly IMergedRepository _mergedRepository;

        public GenericService(IMergedRepository mergedRepository)
        {
            _mergedRepository = mergedRepository;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var repository = _mergedRepository.GetRepository<IGenericRepository<TEntity>>();
            var entities = await repository.GetAllAsync();
            return entities;
        }

        public async Task<IEntity> GetAsync(int id)
        {
            var repository = _mergedRepository.GetRepository<IGenericRepository<TEntity>>();
            var entity = await repository.GetAsync(id);
            return entity;
        }

        public async Task<bool> AddAsync(TModel model)
        {
            var entity = new TEntity();
            entity.CopyValues(model);

            var repository = _mergedRepository.GetRepository<IGenericRepository<TEntity>>();
            await repository.AddAsync(entity);

            var isSuccessSave = await _mergedRepository.SaveEntitiesAsync();

            return isSuccessSave;
        }

        public async Task<bool> UpdateAsync(int id, TModel newModel)
        {
            var repository = _mergedRepository.GetRepository<IGenericRepository<TEntity>>();

            var model = await repository.GetAsync(id);
            model.CopyValues(newModel);

            repository.Update(model);

            var isSuccessSave = await _mergedRepository.SaveEntitiesAsync();
            return isSuccessSave;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var repository = _mergedRepository.GetRepository<IGenericRepository<TEntity>>();
            var model = await repository.GetAsync(id);
            repository.Remove(model);

            var isSuccessSave = await _mergedRepository.SaveEntitiesAsync();
            return isSuccessSave;
        }
    }
}
