using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Manager.SceneConvertManager
{
    public class SceneConvertManager : MonoBehaviour
    {
        public static SceneConvertManager instance { get; private set; }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 씬 전환 (즉시 변환)
        // 로딩 씬이 필요하면 코드 수정 예정
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}



