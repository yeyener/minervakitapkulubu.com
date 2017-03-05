using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.ObjectMapper
{
    public static class IDataReaderExtensions
    {
        public static string ReadString(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetString(dataReader.GetOrdinal(ColumnName));
        }

        public static bool ReadBoolean(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(ColumnName)) != 0;
        }
        public static bool? ReadNullableBoolean(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetInt32(dataReader.GetOrdinal(ColumnName)) != 0;
        }
        public static int ReadInt32(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(ColumnName));
        }
        public static int? ReadNullableInt32(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetInt32(dataReader.GetOrdinal(ColumnName));
        }
        public static long ReadInt64(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(ColumnName));
        }
        public static long? ReadNullableInt64(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetInt64(dataReader.GetOrdinal(ColumnName));
        }
        public static decimal ReadDecimal(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(ColumnName));
        }
        public static decimal? ReadNullableDecimal(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetDecimal(dataReader.GetOrdinal(ColumnName));
        }
        public static float ReadFloat(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetFloat(dataReader.GetOrdinal(ColumnName));
        }
        public static float? ReadNullableFloat(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetFloat(dataReader.GetOrdinal(ColumnName));
        }
        public static double ReadDouble(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetDouble(dataReader.GetOrdinal(ColumnName));
        }
        public static double? ReadNullableDouble(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetDouble(dataReader.GetOrdinal(ColumnName));
        }

        public static DateTime ReadDateTime(this IDataReader dataReader, string ColumnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(ColumnName));
        }
        public static DateTime? ReadNullableDateTime(this IDataReader dataReader, string ColumnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return dataReader.GetDateTime(dataReader.GetOrdinal(ColumnName));
        }

        public static TEnum ReadEnum<TEnum>(this IDataReader dataReader, string ColumnName) where TEnum : struct, IConvertible
        {
            return (TEnum)Enum.Parse(typeof(TEnum), dataReader.GetValue(dataReader.GetOrdinal(ColumnName)).ToString());
        }
        public static TEnum? ReadNullableEnum<TEnum>(this IDataReader dataReader, string ColumnName) where TEnum : struct, IConvertible
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(ColumnName))) return null;
            return (TEnum)Enum.Parse(typeof(TEnum), dataReader.GetValue(dataReader.GetOrdinal(ColumnName)).ToString());
        }
    }
}
