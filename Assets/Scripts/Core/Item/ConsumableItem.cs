using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Item
{
    public class ConsumableItem : Item
    {
        public int Amount { get; private set; }

        public ConsumableItem(string id, string name, Sprite icon, int amount)
            : base(id, name, icon)
        {
            Amount = amount;
        }

        public override void Use()
        {
            if (Amount > 0)
            {
                Amount--;

            }
            else
            {

            }
        }
    }
}

