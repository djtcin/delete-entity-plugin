using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntityPlugin.Common
{
    public class ComboItem<T>
    {
        public string DisplayName { get; set; }
        public T Value { get; set; }

        public ComboItem() { }

        public ComboItem(string name, T value) 
        {
            DisplayName = name;
            Value = value;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
