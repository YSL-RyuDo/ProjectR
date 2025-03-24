using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager.SceneConvertManager;

namespace Core.Interactable.Portal
{
    public class PortalManager : MonoBehaviour
    {
        public static PortalManager instance { get; private set; }  


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);

                SceneManager.sceneLoaded += OnsceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnsceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnsceneLoaded");

            if(SceneConvertManager.instance == null)
            {
                Debug.Log("SceneConvertManager == null");
                return;
            }

            var (fromID, toID) = SceneConvertManager.instance.GetNextSpawnPortal();

            Debug.Log(fromID + toID);

            Portal spawnPortal = FindConnectedPortal(fromID, toID);

            if (spawnPortal != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPortal.transform.position;
                }
            }
        }


        private Portal FindConnectedPortal(string fromID, string toID)
        {
            GameObject[] portalObjects = GameObject.FindGameObjectsWithTag("Portal");
            foreach (GameObject obj in portalObjects)
            {
                Portal portal = obj.GetComponent<Portal>();
                if (portal != null && portal.portalID == toID && portal.targetPortalID == fromID)   
                {
                    return portal;
                }
            }
            return null;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnsceneLoaded;
        }
    }

}


