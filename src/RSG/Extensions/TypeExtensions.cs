using System.Reflection;

namespace RSG.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets all public members of type <typeparamref name="T"/>
        /// given a <paramref name="type"/>.
        /// </summary>
        /// <typeparam name="T">The type of public constants to search.</typeparam>
        /// <param name="type">The class containing the public constants of type
        /// <typeparamref name="T"/>.</param>
        /// <returns>A collection of public constants of type <typeparamref name="T"/>
        /// in the class <paramref name="type"/>.</returns>
        public static IEnumerable<T> GetPublicConstants<T>(this Type type)
        {
            var types =  type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(T))
                .Select(constType => constType.GetRawConstantValue())
                .Cast<T>();

            return types;
        }

        public static IDictionary<string, (string, object)> GetPublicProperties<T>(this Type type)
        {
            var properties = new Dictionary<string, (string, object)>();
            var propertiesToGet = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToArray();

            foreach (var prop in propertiesToGet)
            {
                var propName = prop.Name;
                var propType = prop.GetType();
                var propValue = prop.GetValue(propType);
                properties.Add(propName, (propType.Name, propValue));
            }

            return properties;
        }
    }
}
