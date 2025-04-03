using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Item;

namespace Core.UI.Inventory
{
    public class ItemSlotUI : MonoBehaviour
    {
        public Image iconImage;

        private Core.Item.Item currentItem;

        // 슬롯에 아이템을 설정
        public void SetItem(Core.Item.Item item)
        {
            currentItem = item;
            Debug.Log($"[ItemSlotUI] SetItem 호출됨: {item.Id}, Icon = {item.Icon}");
            Debug.Log(currentItem.Id );
            if (item != null && item.Icon != null)
            {
                Debug.Log($"[ItemSlotUI] 아이콘 적용: {item.Icon.name}");
                iconImage.sprite = item.Icon;
                iconImage.enabled = true;
            }
            else
            {
                Debug.LogWarning($"[ItemSlotUI] 아이콘이 null입니다. item.Icon = {item.Icon}");
                Clear();
            }
        }

        // 슬롯을 비우기
        public void Clear()
        {
            currentItem = null;
            iconImage.sprite = null;
            iconImage.enabled = false;
        }

    }
}
