using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Item
{
    public class MiscItem : Item
    {
        public override ItemType Type => ItemType.Misc;

        public MiscItem(string id, string name, Sprite icon)
            : base(id, name, icon)
        {
        }

        public override void Use()
        {
        }
    }
}
