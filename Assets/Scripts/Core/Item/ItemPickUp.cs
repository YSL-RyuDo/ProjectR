using Core.Data.ItemData;
using System.Collections;
using System.Collections.Generic;
using Core.Inventory;
using Core.Item;
using UnityEngine;
using Manager.DataManger;

// 아이템을 줍는 것이 아니라 떨어진 아이템에 대한 정보임
namespace Core.Item.ItemPickUp
{
    public class ItemPickUp : MonoBehaviour
    {
        private string itemId;
        public void Initialize(string id)
        {
            itemId = id;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Item item = CreateItemFromID(itemId);
            if (item != null)
            {
                Core.Inventory.Inventory.instance.AddItem(item);
                Debug.Log($"[ItemPickUp] ID '{itemId}'에 해당하는 아이템 획득");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning($"[ItemPickUp] ID '{itemId}'에 해당하는 아이템 생성 실패");
            }
        }

        private Item CreateItemFromID(string id)
        {
            if (ItemDataManager.instance.miscItemDataDict.TryGetValue(id, out var miscData))
            {
                return new MiscItem(miscData.id, miscData.itemName, LoadIcon(miscData.iconPath));
            }

            return null;
        }

        private Sprite LoadIcon(string path)
        {
            Debug.Log(path);
            return Resources.Load<Sprite>(path);

        }
    }

}

