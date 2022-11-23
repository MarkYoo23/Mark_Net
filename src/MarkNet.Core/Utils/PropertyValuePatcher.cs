using System.Linq;

namespace MarkNet.Core.Utils
{
    public static class PropertyValuePatcher<TParent, TChild> where TParent : class where TChild : class
    {
        public static void PatchValues(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();
            foreach (var parentProperty in parentProperties)
            {
                var childProperty = childProperties
                    .Where(row => row.Name == parentProperty.Name)
                    .FirstOrDefault();

                if (childProperty == null)
                {
                    continue;
                }

                var parentValue = parentProperty.GetValue(parent);
                if (parentValue == null)
                {
                    continue;
                }

                childProperty.SetValue(child, parentValue);
            }
        }
    }
}
