using UnityEngine;

namespace Core.Item
{
    public abstract class Item 
    {
        public enum ItemType
        {
            All,
            Misc,
            Consumable,
            Equipment
        }

        public string Id { get; protected set; }
        public string ItemName { get; protected set; }

        public Sprite Icon { get; protected set; }

        public abstract ItemType Type { get; }

        public Item(string id, string itemName, Sprite icon)
        {
            Id = id;
            ItemName = itemName;
            Icon = icon;
        }

        public abstract void Use();
    }
}


