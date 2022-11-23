using System.Linq;

namespace MarkNet.Core.Utils
{
    public static class PropertyValueCopier<TParent, TChild> where TParent : class where TChild : class
    {
        public static void CopyValues(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();
            foreach (var parentProperty in parentProperties)
            {
                var childProperty = childProperties
                    .Where(row => row.Name == parentProperty.Name)
                    .Where(row => row.PropertyType == parentProperty.PropertyType)
                    .FirstOrDefault();

                if (childProperty == null)
                {
                    continue;
                }

                var parentValue = parentProperty.GetValue(parent);
                childProperty.SetValue(child, parentValue);
            }
        }
    }
}
