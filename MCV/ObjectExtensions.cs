using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MCV
{
    public static class ObjectExtensions
    {
        private static StringOptions? _stroptions;
        private static EnumerableOptions? _enumerableoptions;
        private static List<int> _tracker;
        private static string _id;
        public static bool HasNullOrEmptyProperties(this object obj, Configuration configuration = null)
        {
            try
            {
                if (obj == null)
                    return true;

                if (configuration == null)
                    configuration = getDefaultConfiguration();
                Init(configuration.CustomIdentificationProperty, configuration.StringOptions, configuration.EnumerableOptions);

                if (isBuiltInType(obj.GetType()))
                    return check(obj);
                else
                {
                    _tracker.Add(identification(obj));
                    var properties = getProperties(obj)
                    .ToList();

                    return properties.Any(a => check(a.GetValue(obj), a.GetCustomAttributes()));
                }
            }
            finally
            {
                Dispose();
            }
        }

        private static Configuration getDefaultConfiguration()
        {
            return new Configuration()
            {
                CustomIdentificationProperty = null,
                StringOptions = 0,
                EnumerableOptions = 0
            };
        }

        private static void Init(string id_property, StringOptions str, EnumerableOptions enumr)
        {
            if (_id == null)
                _id = id_property;

            if (_stroptions == null)
                _stroptions = str;

            if (_enumerableoptions == null)
                _enumerableoptions = enumr;

            if (_tracker == null)
                _tracker = new List<int>();
        }

        private static void Dispose()
        {
            _stroptions = null;
            _enumerableoptions = null;
            _id = null;
            _tracker = null;
        }

        private static bool isBuiltInType(Type type) => type.Namespace.StartsWith("System");

        private static bool check(object value, IEnumerable<Attribute> attributes = null)
        {
            if (value == null)
                return true;

            if (value is string s)
                if ((_stroptions & StringOptions.DenyEmptyStrings) > 0)
                    return s.Equals(string.Empty);
                else
                    return false;
            
            if (value is IEnumerable a)
            {
                if ((_enumerableoptions & EnumerableOptions.DenyEmptyEnumerables) > 0)
                    if (a.Count() == 0)
                        return true;
                if ((_enumerableoptions & EnumerableOptions.CheckEnumerableElements) > 0)
                    foreach (var i in a)
                        if (_tracker.Contains(identification(i)))
                            continue;
                        else if (i.HasNullOrEmptyProperties())
                            return true;
            }

            var type = value.GetType();

            var default_value = attributes?
                .FirstOrDefault(a => a is MCV_DefaultValue) as MCV_DefaultValue;

            if (default_value != null)
                return value.Equals(default_value.obj);

            if (!isBuiltInType(type) && !_tracker.Contains(identification(value)))
                return value.HasNullOrEmptyProperties();

            return value.Equals(Activator.CreateInstance(type));
        }

        private static int identification(object value)
        {
            if (_id != null && _id != string.Empty)
                return (int)value.GetType().GetProperty(_id).GetValue(value);
            else
                return value.GetHashCode();
        }

        private static IEnumerable<PropertyInfo> getProperties(object obj)
            => obj.GetType()
            .GetProperties()
            .Where(a => !a.GetCustomAttributes().Any(b => b is MCV_Ignore));
    }


    public static class EnumerableExtensions
    {
        public static int Count(this IEnumerable obj)
        {
            int result = 0;
            foreach (var i in obj)
                result += 1;
            return result;
        }
    }
}
