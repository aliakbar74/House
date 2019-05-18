namespace House.Extensions {
    public static class ReflectionExtension {
        public static string GetPropertyValue<T>(this T item, string propName) {
            return item.GetType().GetProperty(propName).GetValue(item, null).ToString();
        }
    }
}