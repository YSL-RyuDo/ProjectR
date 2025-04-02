using Core.Data.ItemData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utility.Data;
using UnityEngine;

namespace Manager.DataManger
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }

        public Dictionary<string, ItemData> itemDataDict;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadItemData();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void LoadItemData()
        {
            var textAsset = Resources.Load<TextAsset>("DataTable/ConsumableItemData");
            var list = DataParser.ParseArray<ItemData>(textAsset);
            itemDataDict = list.ToDictionary(item => item.id);

            Debug.Log("<color=cyan>[DataManager]</color> 소비 아이템 로딩 완료:");
            foreach (var item in itemDataDict.Values)
            {
                Debug.Log($"ID: {item.id}, 이름: {item.itemName}");
            }
        }
    }
}

