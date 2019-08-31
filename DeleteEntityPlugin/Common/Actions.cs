using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntityPlugin.Common
{
    public class Actions
    {
        public enum ActionType
        {
            RenameEntity,
            DeleteEntity,
            RenameAttribute,
            DeleteAttribute
        }
        public static ComboItem<ActionType>[] GetCustomActions()
        {
            return new ComboItem<ActionType>[]
            {
                new ComboItem<ActionType>("Rename entity", ActionType.RenameEntity),
                new ComboItem<ActionType>("Delete entity", ActionType.DeleteEntity),
                new ComboItem<ActionType>("Rename attribute", ActionType.RenameAttribute),
                new ComboItem<ActionType>("Delete attribute", ActionType.DeleteAttribute)
            };
        }

        public static ComboItem<ActionType>[] GetNotCustomActions()
        {
            return new ComboItem<ActionType>[]
            {
                new ComboItem<ActionType>("Rename attribute", ActionType.RenameAttribute),
                new ComboItem<ActionType>("Delete attribute", ActionType.DeleteAttribute)
            };
        }
    }
}
