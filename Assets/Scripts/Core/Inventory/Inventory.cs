using System.Collections;
using System.Collections.Generic;
using Core.Item;
using UnityEngine;

namespace Core.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance { get; private set; }

        private List<Core.Item.Item> items = new();

        [SerializeField] private int maxSlotCount = 20;

        public IReadOnlyList<Core.Item.Item> Items => items;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        public bool AddItem(Core.Item.Item newItem)
        {
            if (items.Count >= maxSlotCount)
            {
                Debug.Log("인벤토리 가득 참");
                return false;
            }

            Debug.Log("아이템 획득");
            items.Add(newItem);
            Core.UI.Inventory.InventoryUI.instance?.Refresh();
            return true;
        }

        // 아이템 제거
        public bool RemoveItem(Core.Item.Item item)
        {
            return items.Remove(item);
        }

        // 특정 인덱스에 있는 아이템 반환
        public Core.Item.Item GetItem(int index)
        {
            if (index < 0 || index >= items.Count)
                return null;
            return items[index];
        }

        // 슬롯 비우기
        public void ClearInventory()
        {
            items.Clear();
        }


    }

}


