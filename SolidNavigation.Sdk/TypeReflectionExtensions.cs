using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SolidNavigation.Sdk
{
    public static class TypeReflectionExtensions
    {
        /// <summary>
        /// Returns the declared properties of a type or its base types.
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <returns>An enumerable of the <see cref="PropertyInfo"/> objects.</returns>
        public static IEnumerable<PropertyInfo> GetPropertiesHierarchical(this Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            if (type.Equals(typeof(object)))
            {
                return type.GetTypeInfo().DeclaredProperties;
            }

            return type.GetTypeInfo().DeclaredProperties.Concat(GetPropertiesHierarchical(type.GetTypeInfo().BaseType));
        }
    }

}
