using MarkNet.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Cashings
{
    public class CashManager<T> where T : PropertyModel<T>, new()
    {
        private const int _millisecondsWriteTimeout = 1000;
        private const int _millisecondsReadDelay = 1;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
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

        private async Task WaitCanReadAsync()
        {
            while (!IsReadable()) 
            {
                await Task.Delay(_millisecondsReadDelay);
            }
        }

        private bool IsReadable()
        {
            return _semaphoreSlim.CurrentCount > 0;
        }

        public async Task<bool> SetAsync(T model)
        {
            var newModel = new T();
            newModel.CopyValues(model);

            if (!await _semaphoreSlim.WaitAsync(CashManager<T>._millisecondsWriteTimeout))
            {
                return false;
            }

            _model = newModel;

            _semaphoreSlim.Release();

            return true;
        }

        public async Task<bool> PatchAsync(T model)
        {
            if (!await _semaphoreSlim.WaitAsync(CashManager<T>._millisecondsWriteTimeout))
            {
                return false;
            }

            _model.PatchValues(model);

            _semaphoreSlim.Release();
            return true;
        }
    }
}
