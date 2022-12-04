using MarkNet.Core.Models;
using MarkNet.Core.Services.Commons;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Cashings
{
    public class CashManager<T> : AsyncLockManager where T : PropertyModel<T>, new()
    {
        private T _model;

        public CashManager()
        {
            _model = new T();
        }

        public CashManager(T model)
        {
            _model = model;
        }

        public T Get()
        {
            WaitCanReadAsync().Wait();

            var model = _model;
            return model.Clone();
        }

        public async Task<T> GetAsync()
        {
            await WaitCanReadAsync();

            var model = _model;
            return model.Clone();
        }

        public async Task<bool> SetAsync(T model)
        {
            var newModel = new T();
            newModel.CopyValues(model);

            if (!await _semaphoreSlim.WaitAsync(_millisecondsWriteTimeout))
            {
                return false;
            }

            _model = newModel;

            _semaphoreSlim.Release();

            return true;
        }

        public async Task<bool> PatchAsync(T model)
        {
            if (!await _semaphoreSlim.WaitAsync(_millisecondsWriteTimeout))
            {
                return false;
            }

            _model.PatchValues(model);

            _semaphoreSlim.Release();
            return true;
        }
    }
}
