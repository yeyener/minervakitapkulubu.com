using System;

namespace Evorine.Data.ObjectMapper
{
    public class ColumnBindAttribute : Attribute
    {
        public ColumnBindAttribute()
        {
            Prefix = null;
            Name = null;
            Enabled = true;
        }

        public ColumnBindAttribute(string name)
        {
            Name = name;
            Enabled = true;
        }

        public ColumnBindAttribute(bool enabled)
        {
            Name = null;
            Enabled = enabled;
        }

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Prefix { get; set; }
    }
}