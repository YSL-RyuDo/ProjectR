using UnityEngine;

namespace Core.Item
{
    public abstract class Item 
    {
        public string Id { get; protected set; }
        public string ItemName { get; protected set; }

        public Sprite Icon { get; protected set; }

        public Item(string id, string itemName, Sprite icon)
        {
            Id = id;
            ItemName = itemName;
            Icon = icon;
        }

        public abstract void Use();
    }
}


