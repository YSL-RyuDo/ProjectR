using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_MoveLeftState : TutorialState
    {
        private GameObject platform;
        private TargetPlatform platformScript;
        private GameObject leftBlock;

        private Vector3 targetPosition;

        public void EnterState(TutorialManager tutorial)
        {
            Debug.Log("¿ÞÂÊ ÀÌµ¿");
            leftBlock = GameObject.Find("LeftTargetBlock");
            targetPosition = leftBlock.transform.position;

            platform = tutorial.GetPlatform(targetPosition);
            platformScript = platform.GetComponent<TargetPlatform>();
        }

        public void UpdateState(TutorialManager tutorial)
        {
            if (platformScript != null && platformScript.IsSteppedOn())
            {
                platformScript.ResetPlatform();
                tutorial.SetState(new Tutorial1_TalkWithNPCState(TutorialStep.MoveToPortal));
            }
        }
    }
}

