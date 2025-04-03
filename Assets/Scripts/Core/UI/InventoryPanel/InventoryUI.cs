// InventoryUI.cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Core.Item;
using static Core.Item.Item;

namespace Core.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject slotPrefab;
        public Transform slotParent; // Grid Layout이 붙어 있는 Content 영역

        public Button allButton;
        public Button miscButton;
        public Button consumableButton;
        public Button equipmentButton;

        private List<ItemSlotUI> slotUIs = new();
        private ItemType currentFilter = ItemType.All;

        private int maxSlotCount = 30;

        public static InventoryUI instance;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            // 필터 버튼 연결
            allButton.onClick.AddListener(() => ApplyFilter(ItemType.All));
            miscButton.onClick.AddListener(() => ApplyFilter(ItemType.Misc));
            consumableButton.onClick.AddListener(() => ApplyFilter(ItemType.Consumable));
            equipmentButton.onClick.AddListener(() => ApplyFilter(ItemType.Equipment));

            ApplyFilter(ItemType.All);
        }

        public void ApplyFilter(ItemType type)
        {
            currentFilter = type;
            Refresh();
        }

        public void Refresh()
        {
            var items = Core.Inventory.Inventory.instance.Items;

            var filtered = items.Where(item => currentFilter == ItemType.All || item.Type == currentFilter).ToList();

            while (slotUIs.Count < maxSlotCount)
            {
                var newSlot = Instantiate(slotPrefab, slotParent);
                var ui = newSlot.GetComponent<ItemSlotUI>();
                slotUIs.Add(ui);
            }

            // 슬롯 채우기
            for (int i = 0; i < slotUIs.Count; i++)
            {
                if (i < filtered.Count)
                    slotUIs[i].SetItem(filtered[i]);
                else
                    slotUIs[i].Clear();
            }

            Debug.Log($"[UI] 슬롯 생성됨: {slotUIs.Count}개");
        }
    }
}
