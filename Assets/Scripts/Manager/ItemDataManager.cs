using Core.Data.ItemData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utility.Data;
using UnityEngine;

namespace Manager.DataManger
{
    public class ItemDataManager : MonoBehaviour
    {
        public static ItemDataManager instance { get; private set; }

        public Dictionary<string, ItemData> consumableItemDataDict;
        public Dictionary<string, ItemData> miscItemDataDict;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                LoadConsumableItemData();
                LoadMiscItemData();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void LoadConsumableItemData()
        {
            var textAsset = Resources.Load<TextAsset>("DataTable/ConsumableItemData");
            var list = DataParser.ParseArray<ItemData>(textAsset);
            consumableItemDataDict = list.ToDictionary(item => item.id);

            Debug.Log("소비 아이템 로딩 완료:");
            foreach (var item in consumableItemDataDict.Values)
            {
                Debug.Log($"ID: {item.id}, 이름: {item.itemName}");
            }
        }

        void LoadMiscItemData()
        {
            var textAsset = Resources.Load<TextAsset>("DataTable/MiscItemData");
            var list = DataParser.ParseArray<ItemData>(textAsset);
            miscItemDataDict = list.ToDictionary(item => item.id);

            Debug.Log("기타 아이템 로딩 완료:");
            foreach (var item in miscItemDataDict.Values)
            {
                Debug.Log($"ID: {item.id}, 이름: {item.itemName}");
            }
        }
    }
}

