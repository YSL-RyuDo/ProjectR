using System.Collections;
using System.Collections.Generic;
using Core.Data.ItemData;
using Core.Item.ItemPickUp;
using Manager.DataManger;
using UnityEngine;

namespace Core.Interactable.Bush
{
    public class BushInteractable : MonoBehaviour, Interactable
    {
        public string itemId;

        public GameObject itemDropPrefab;

        public Transform spawnPoint;
        
        public void Interact()
        {
            SpawnItem();
            Destroy(gameObject); // 풀숲 제거
        }

        void SpawnItem()
        {
            Debug.LogWarning($" {itemId}인 아이템을 소환");
            if (!ItemDataManager.instance.miscItemDataDict.TryGetValue(itemId, out var itemData))
            {
                Debug.LogWarning($"ID가 {itemId}인 아이템을 찾을 수 없습니다.");
                return;
            }

            var itemObj = Instantiate(itemDropPrefab, spawnPoint.position, Quaternion.identity);

            var pickup = itemObj.GetComponent<ItemPickUp>();
            if (pickup != null)
            {
                pickup.Initialize(itemId);
            }
            else
            {
                Debug.LogWarning("ItemPickup 컴포넌트가 없");
            }
        }
    }
}

