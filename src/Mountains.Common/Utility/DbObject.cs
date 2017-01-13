using System.Collections.Generic;
using System.Linq;

namespace Mountains.Common.Utility
{
    public abstract class DbObject<T>
    {
        public static string GenerateColumns(string alias = null)
        {
            string aliasAddition = alias != null ? alias + "." : "";
            return GetPropertyNames().Select(x => aliasAddition + x).Aggregate((i, j) => i + ", " + j);
        }

        private static ICollection<string> GetPropertyNames()
        {
            if (_propertyNames == null)
                _propertyNames = typeof(T).GetProperties().Select(x => x.Name).ToList();

            return _propertyNames;
        }

        private static ICollection<string> _propertyNames;
    }
}
