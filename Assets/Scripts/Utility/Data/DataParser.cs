using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Data
{
    public static class DataParser
    {
        // 배열 JSON 파싱
        public static List<T> ParseArray<T>(string json)
        {
            string wrapped = WrapArray(json);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrapped);
            return new List<T>(wrapper.items);
        }

        public static List<T> ParseArray<T>(TextAsset jsonAsset)
        {
            return ParseArray<T>(jsonAsset.text);
        }

        // 단일 JSON 파싱
        public static T ParseSingle<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static T ParseSingle<T>(TextAsset jsonAsset)
        {
            return ParseSingle<T>(jsonAsset.text);
        }

        // JsonUtility가 배열을 못 읽으므로 래핑 처리
        private static string WrapArray(string jsonArray)
        {
            return "{\"items\":" + jsonArray + "}";
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }
    }
}
