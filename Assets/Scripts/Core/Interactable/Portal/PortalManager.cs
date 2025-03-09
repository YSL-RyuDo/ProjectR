using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.SceneConvertManager;

namespace Core.Interactable.Portal
{
    public class PortalManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string spawnPortalID = SceneConvertManager.instance.GetNextSpawnPortal();
            if (!string.IsNullOrEmpty(spawnPortalID))
            {
                Portal spawnPortal = FindSpawnPortal(spawnPortalID);
                if (spawnPortal != null)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        player.transform.position = spawnPortal.transform.position;
                        Debug.Log($"플레이어가 {spawnPortalID} 포탈에서 스폰됨");
                    }
                }
            }
        }

        private Portal FindSpawnPortal(string portalID)
        {
            GameObject[] portalObjects = GameObject.FindGameObjectsWithTag("Portal");
            foreach (GameObject obj in portalObjects)
            {
                Portal portal = obj.GetComponent<Portal>();
                if (portal != null && portal.portalID == portalID)
                {
                    return portal;
                }
            }
            return null;
        }
    }

}


