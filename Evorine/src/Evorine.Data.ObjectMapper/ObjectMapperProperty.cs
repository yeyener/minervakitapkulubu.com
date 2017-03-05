using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Evorine.Data.ObjectMapper
{
    internal class ObjectMapperProperty
    {
        public string PropertyName;
        public string ColumnName;
        public bool Enabled;

        public PropertyInfo Property;
        public FieldInfo Field;

        public PropertyInfo SubClassProperty;
        public FieldInfo SubClassField;

        internal static IList<ObjectMapperProperty> InstantiatePropertyList(Type type)
        {
            var infos = new List<ObjectMapperProperty>();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(ColumnBindAttribute)) as ColumnBindAttribute;
                if (attribute == null)
                {
                    infos.Add(new ObjectMapperProperty
                    {
                        PropertyName = property.Name,
                        ColumnName = property.Name,
                        Enabled = true,
                        Property = property
                    });
                }
                else if (attribute.Prefix == null)
                {
                    infos.Add(new ObjectMapperProperty
                    {
                        PropertyName = property.Name,
                        ColumnName = attribute.Name,
                        Enabled = attribute.Enabled,
                        Property = property
                    });
                }
                else
                {
                    var subInfos = InstantiatePropertyList(property.PropertyType);
                    foreach (var info in subInfos)
                        info.SubClassProperty = property;
                    infos.AddRange(subInfos);
                }
            }

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute(typeof(ColumnBindAttribute)) as ColumnBindAttribute;
                if (attribute == null)
                {
                    infos.Add(new ObjectMapperProperty
                    {
                        PropertyName = field.Name,
                        ColumnName = field.Name,
                        Enabled = true,
                        Field = field
                    });
                }
                else if (attribute.Prefix == null)
                {
                    infos.Add(new ObjectMapperProperty
                    {
                        PropertyName = field.Name,
                        ColumnName = attribute.Name,
                        Enabled = attribute.Enabled,
                        Field = field
                    });
                }
                else
                {
                    var subInfos = InstantiatePropertyList(field.FieldType);
                    foreach (var info in subInfos)
                        info.SubClassField = field;
                    infos.AddRange(subInfos);
                }
            }

            return infos;
        }

        public Action<object, IDataReader, int> SetAction { get; set; }

        Type type;
        public Type Type
        {
            get
            {
                if (type != null) return type;
                type = Property != null ? Property.PropertyType : Field.FieldType;
                return type;
            }
        }

        public bool HasSubClass { get { return SubClassProperty != null || SubClassField != null; } }

        public void SetValue(object obj, object value)
        {
            try
            {
                if (Property != null) Property.SetValue(obj, value);
                else Field.SetValue(obj, value);
            }
            catch(ArgumentException e)
            {
                throw new ArgumentException(e.Message, PropertyName, e);
            }
        }

        public object GetSubClassValue(object obj)
        {
            if (HasSubClass)
            {
                if (SubClassProperty != null) return SubClassProperty.GetValue(obj);
                else return SubClassField.GetValue(obj);
            }
            else return null;
        }

        public void SetSubClassValue(object obj, object value)
        {
            if (HasSubClass)
            {
                if (SubClassProperty != null) SubClassProperty.SetValue(obj, value);
                else SubClassField.SetValue(obj, value);
            }
        }
    }
}