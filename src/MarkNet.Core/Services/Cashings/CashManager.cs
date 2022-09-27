using MarkNet.Core.Models;

namespace MarkNet.Core.Services.Cashings
{
    public class CashManager<T> where T : PropertyModel<T>, new()
    {
        private object _locker = new object();
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
            lock (_locker)
            {
                return _model.Clone();
            }
        }

        public void Set(T model)
        {
            lock (_locker)
            {
                _model.CopyValues(model);
            }
        }

        public void Patch(T model)
        {
            lock (_locker)
            {
                _model.PatchValues(model);
            }
        }
    }
}
