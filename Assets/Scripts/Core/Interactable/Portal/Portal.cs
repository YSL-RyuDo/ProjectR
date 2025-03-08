using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Manager.SceneConvertManager;

namespace Core.Interactable.Portal
{
    public class Portal : MonoBehaviour, Interactable
    {
        public string targetSceneName; // ÀÌµ¿ÇÒ ¾À ÀÌ¸§

        public void Interact()
        {
            Debug.Log("Æ÷Å»");
            
            SceneConvertManager.instance.LoadScene(targetSceneName);
        }
    }
}


