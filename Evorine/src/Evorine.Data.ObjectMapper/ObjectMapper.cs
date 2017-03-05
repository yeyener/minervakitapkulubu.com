using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Evorine.Data.ObjectMapper
{
    public class ObjectMapper<T> where T : new()
    {
        private static bool IsInitialized = false;
        private static object isInitializedLocker = new object();
        private static IList<ObjectMapperProperty> Infos = null;

        private ObjectMapper()
        {
        }

        public static ObjectMapper<T> Instantiate()
        {
            return new ObjectMapper<T>();
        }
        
        public T Map(IDataReader DR)
        {
            if (!IsInitialized) Initialize();

            T obj = new T();
            var columnSchema = ((DbDataReader)DR).GetColumnSchema();
            var columns = columnSchema.Select(x => new { ColumnName = (string)x.ColumnName, Ordinal = x.ColumnOrdinal.Value });
            foreach (var property in Infos)
            {
                int i;
                var columnInfo = columnSchema.FirstOrDefault(x => string.Equals(x.ColumnName, property.ColumnName, StringComparison.OrdinalIgnoreCase));
                if (columnInfo == null) continue;

                Type type = property.Type;

                if (type.GetTypeInfo().IsGenericType)
                    if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        type = type.GenericTypeArguments.First();

                try
                {
                    if (type == null) continue;

                    if (property.HasSubClass)
                    {
                        var subObj = property.GetSubClassValue(obj);
                        if (subObj == null)
                        {
                            subObj = Activator.CreateInstance(property.SubClassProperty.PropertyType);
                            property.SetSubClassValue(obj, subObj);
                        }
                        setPropertyValue(subObj, property, DR, columnInfo.ColumnOrdinal.Value);
                    }
                    else
                    {
                        setPropertyValue(obj, property, DR, columnInfo.ColumnOrdinal.Value);
                    }
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException("ColumnName: " + property.ColumnName);
                }
                catch (ArgumentNullException e)
                {
                    throw e;
                }
                catch (FormatException e)
                {
                    throw e;
                }
            }

            return obj;
        }

        public Task<T> MapAsync(IDataReader DR)
        {
            return Task.Run(() => { return Map(DR); });
        }

        private void setPropertyValue(object obj, ObjectMapperProperty property, IDataReader DR, int index)
        {
            var type = property.Type;

            if (DR.IsDBNull(index)) property.Property.SetValue(obj, null);
            else property.SetAction(obj, DR, index);
        }

        public IList<T> ReadAndMap(IDataReader DR)
        {
            var list = new List<T>();
            try
            {
                while (DR.Read())
                {
                    list.Add(Map(DR));
                }
            }
            catch
            {
            }
            return list;
        }

        public T ReadAndMapFirst(IDataReader DR)
        {
            try
            {
                if (DR.Read())
                    return Map(DR);
                return default(T);
            }
            catch
            {
                throw;
            }
        }

        private static void Initialize()
        {
            lock (isInitializedLocker)
            {
                if (IsInitialized) return;

                Infos = ObjectMapperProperty.InstantiatePropertyList(typeof(T));
                setSetterActions();

                IsInitialized = true;
            }
        }

        private static void setSetterActions()
        {
            foreach(var property in Infos)
            {
                var type = property.Type;

                // Numbers
                if (type == typeof(Int32) || type == typeof(Int32?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetInt32(index)); };
                else if (type == typeof(Int16) || type == typeof(Int16?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetInt16(index)); };
                else if (type == typeof(Int64) || type == typeof(Int64?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetInt64(index)); };
                else if (type == typeof(float) || type == typeof(float?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetFloat(index)); };
                else if (type == typeof(double) || type == typeof(double?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetDouble(index)); };
                else if (type == typeof(decimal) || type == typeof(decimal?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetDecimal(index)); };

                // Others
                else if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    property.SetAction = (object obj, IDataReader reader, int index) => {
                        try
                        {
                            property.Property.SetValue(obj, reader.GetDateTime(index));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            throw new ArgumentOutOfRangeException(property.PropertyName, reader.GetString(index), e.Message);
                        }
                    };
                }
                else if (type == typeof(bool) || type == typeof(bool?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetInt32(index) == 0 ? false : true); };
                else if (type == typeof(char) || type == typeof(char?))
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, char.Parse(reader.GetString(index))); };
                else if (type.GetTypeInfo().BaseType == typeof(Enum))
                {
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, Enum.Parse(type, reader.GetValue(index).ToString())); };
                }
                else if (Nullable.GetUnderlyingType(type) != null && Nullable.GetUnderlyingType(type).GetTypeInfo().IsEnum)
                {
                    property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, Enum.Parse(Nullable.GetUnderlyingType(type), reader.GetValue(index).ToString())); };
                }
                else property.SetAction = (object obj, IDataReader reader, int index) => { property.Property.SetValue(obj, reader.GetString(index)); };
            }
        }
    }
}