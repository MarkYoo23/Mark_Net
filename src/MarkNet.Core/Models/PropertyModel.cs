using MarkNet.Core.Utils;

namespace MarkNet.Core.Models
{
    public abstract class PropertyModel<T> where T : class
    {
#pragma warning disable CS8604
        public T Clone()
        {
            return (T)MemberwiseClone();
        }

        public void CopyValues(T source)
        {
            PropertyValueCopier<T, T>.CopyValues(source, this as T);
        }

        public void PatchValues(T source)
        {
            PropertyValuePatcher<T, T>.PatchValues(source, this as T);
        }

        public void ClearValues() 
        {
            PropertyValueClearer<T>.Clear(this as T);
        }
#pragma warning restore CS8604
    }
}
