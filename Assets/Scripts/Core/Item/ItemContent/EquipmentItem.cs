using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Item
{
    public enum EquipmentType { Weapon, Armor, Accessory }
    public class EquipmentItem : Item
    {
        public override ItemType Type => ItemType.Equipment;
        public EquipmentType EquipType { get; private set; }

        public EquipmentItem(string id, string name, Sprite icon, EquipmentType equipType)
       : base(id, name, icon)
        {
            EquipType = equipType;
        }

        public override void Use()
        {

        }
    }
}

