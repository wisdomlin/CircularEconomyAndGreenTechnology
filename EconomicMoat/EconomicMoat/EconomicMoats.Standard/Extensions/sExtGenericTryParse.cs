using System;
using System.ComponentModel;

namespace EconomicMoats.Standard
{
    public static class sExtGenericTryParse
    {
        public static bool GenericTryParse<T>(this string input, out T result)
        {
            result = default(T);

            //var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                //result = (T)converter.ConvertFromString(input);
                result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    return (T)converter.ConvertFromString(input);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        public static bool Is(this string input, Type targetType)
        {
            try
            {
                TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
