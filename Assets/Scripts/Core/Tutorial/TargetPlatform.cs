using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class TargetPlatform : MonoBehaviour
    {
        private bool isSteppedOn = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isSteppedOn = true;
            }
        }

        public bool IsSteppedOn()
        {
            return isSteppedOn;
        }

        public void ResetPlatform()
        {
            isSteppedOn = false;
            gameObject.SetActive(false);
            TutorialManager.instance.ReturnPlatform(gameObject);
        }
    }

}

