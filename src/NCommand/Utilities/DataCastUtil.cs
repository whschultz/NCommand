using System;

namespace Tectil.NCommand.Utilities
{
    /// <summary>
    /// Casting utility.
    /// </summary>
    internal static class DataCastUtil
    {
        /// <summary>
        /// Converts strings to specified type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object Convert(string value, Type type)
        {
            if (type == typeof(Int16))
                return Int16.Parse(value);
            if (type == typeof(Int32))
                return Int32.Parse(value);
            if (type == typeof(Int64))
                return Int64.Parse(value);

            if (type == typeof(UInt16))
                return UInt16.Parse(value);
            if (type == typeof(UInt32))
                return UInt32.Parse(value);
            if (type == typeof(UInt64))
                return UInt64.Parse(value);

            if (type == typeof(Decimal))
                return Decimal.Parse(value);

            if (type == typeof(Boolean))
                return Boolean.Parse(value);

            if (type == typeof(DateTime))
                return DateTime.Parse(value);

            return value;
        }

    }
}
