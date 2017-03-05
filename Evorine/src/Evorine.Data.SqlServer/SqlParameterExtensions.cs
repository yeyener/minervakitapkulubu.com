using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data
{
    public static class SqlParameterExtensions
    {
        public static decimal AsDecimal(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) throw new NullReferenceException(string.Format("DBNull returned. Parameter name: '{0}'", parameter.ParameterName));
            return (decimal)parameter.Value;
        }

        public static decimal? AsNullableDecimal(this SqlParameter parameter)
        {
            if (parameter.Value == DBNull.Value) return null;
            return (decimal)parameter.Value;
        }

        public static bool AsBoolean(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) throw new NullReferenceException(string.Format("DBNull returned. Parameter name: '{0}'", parameter.ParameterName));
            return (bool)parameter.Value;
        }
        public static bool? AsNullableBoolean(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) return null;
            return (bool)parameter.Value;
        }

        public static int AsInteger(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) throw new NullReferenceException(string.Format("DBNull returned. Parameter name: '{0}'", parameter.ParameterName));
            return (int)parameter.Value;
        }

        public static int? AsNullableInteger(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) return null;
            return (int)parameter.Value;
        }

        public static DateTime AsDateTime(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) throw new NullReferenceException(string.Format("DBNull returned. Parameter name: '{0}'", parameter.ParameterName));
            return (DateTime)parameter.Value;
        }

        public static DateTime? AsNullableDateTime(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) return null;
            return (DateTime)parameter.Value;
        }


        public static T AsEnum<T>(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) throw new NullReferenceException(string.Format("DBNull returned. Parameter name: '{0}'", parameter.ParameterName));
            return (T)parameter.Value;
        }
        public static T? AsNullableEnum<T>(this IDbDataParameter parameter) where T : struct
        {
            if (parameter.Value == DBNull.Value) return null;
            T value;
            if (Enum.TryParse(parameter.Value.ToString(), out value)) return value;
            throw new InvalidCastException(string.Format("Invalid value for type '{0}'. Value: '{1}'.", typeof(T).FullName, value.ToString()));
        }


        public static string AsString(this IDbDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value) return null;
            return (string)parameter.Value;
        }
    }
}
