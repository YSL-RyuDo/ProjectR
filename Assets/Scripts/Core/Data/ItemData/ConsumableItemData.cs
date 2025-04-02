using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data.ItemData
{
    [System.Serializable]
    public class ConsumableItemData : ItemData
    {
        public int maxStack;
        public int healAmount;
    }
}

