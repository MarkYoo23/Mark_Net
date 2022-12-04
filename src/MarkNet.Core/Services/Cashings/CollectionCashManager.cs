using MarkNet.Core.Models;
using MarkNet.Core.Services.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Cashings
{
    public class CollectionCashManager<T> : AsyncLockManager
        where T : PropertyModel<T>, new()
    {
        private List<T> _models = new List<T>();

        public CollectionCashManager()
        {
        }

        public CollectionCashManager(IEnumerable<T> models)
        {
            _models = models.ToList();
        }

        public IEnumerable<T> Get()
        {
            return GetAsync().Result;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            await WaitCanReadAsync();

            var models = _models;
            return models.Select(row => row.Clone()).ToArray();
        }

        public async Task<bool> SetAsync(IEnumerable<T> collections)
        {
            var models = new List<T>();

            foreach (var collection in collections)
            {
                var model = new T();
                model.CopyValues(collection);
                models.Add(model);
            }

            if (!await _semaphoreSlim.WaitAsync(_millisecondsWriteTimeout))
            {
                return false;
            }

            _models = models;

            _semaphoreSlim.Release();

            return true;
        }
    }
}
