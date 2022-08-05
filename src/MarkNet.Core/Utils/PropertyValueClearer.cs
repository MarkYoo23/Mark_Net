namespace MarkNet.Core.Utils
{
    internal static class PropertyValueClearer<T> where T : class
    {
        public static void Clear(T propertyModel) 
        {
            var properties = propertyModel.GetType().GetProperties();

            foreach (var property in properties)
            {
                property.SetValue(propertyModel, null);
            }
        }
    }
}
