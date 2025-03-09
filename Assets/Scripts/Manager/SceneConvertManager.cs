using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Manager.SceneConvertManager
{
    public class SceneConvertManager : MonoBehaviour
    {
        public static SceneConvertManager instance { get; private set; }

        [SerializeField]
        private string nextSpawnPortalID; // 다음 씬에서 플레이어가 스폰될 포탈 ID

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

        public void SetNextSpawnPortal(string portalID)
        {
            nextSpawnPortalID = portalID;
        }

        public string GetNextSpawnPortal()
        {
            return nextSpawnPortalID;
        }
    }

}



