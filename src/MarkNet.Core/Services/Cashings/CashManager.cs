using MarkNet.Core.Models;
using MarkNet.Core.Models.Cashing;

namespace MarkNet.Core.Services.Cashings
{
    public class CashManager<T> where T : PropertyModel<T>, new()
    {
        private const int _millisecondsTimeout = 1000;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
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
            T result;

            _semaphoreSlim.Wait();

            result = _model.Clone();

            _semaphoreSlim.Release();
            return result;
        }

        public async Task<GetCashingResponse<T>> GetAsync()
        {
            T model = null!;

            if (!await _semaphoreSlim.WaitAsync(CashManager<T>._millisecondsTimeout))
            {
                return new GetCashingResponse<T>()
                {
                    IsSuccess = false,
                    Model = null!,
                };
            }

            model = _model.Clone();

            _semaphoreSlim.Release();

            return new GetCashingResponse<T>() 
            {
                IsSuccess = true,
                Model = model,
            };
        }

        public void Set(T model)
        {
            _semaphoreSlim.Wait();

            _model.CopyValues(model);

            _semaphoreSlim.Release();
        }

        public async Task<bool> SetAsync(T model)
        {
            var newModel = new T();
            newModel.CopyValues(model);

            if (!await _semaphoreSlim.WaitAsync(CashManager<T>._millisecondsTimeout))
            {
                return false;
            }

            _model = newModel;

            _semaphoreSlim.Release();

            return true;
        }

        public void Patch(T model)
        {
            _semaphoreSlim.Wait();

            _model.PatchValues(model);

            _semaphoreSlim.Release();
        }

        public async Task<bool> PatchAsync(T model)
        {
            if (!await _semaphoreSlim.WaitAsync(CashManager<T>._millisecondsTimeout))
            {
                return false;
            }

            _model.PatchValues(model);

            _semaphoreSlim.Release();
            return true;
        }
    }
}
