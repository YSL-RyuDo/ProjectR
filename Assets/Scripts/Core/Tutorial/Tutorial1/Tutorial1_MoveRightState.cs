using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_MoveRightState : TutorialState
    {
        private GameObject platform;
        private TargetPlatform platformScript;
        private GameObject rightBlock;

        private Vector3 targetPosition;

        public void EnterState(TutorialManager tutorial)
        {
            Debug.Log("오른쪽 이동");
            rightBlock = GameObject.Find("RightTargetBlock");
            targetPosition = rightBlock.transform.position;

            platform = tutorial.GetPlatform(targetPosition);
            platformScript = platform.GetComponent<TargetPlatform>();
            tutorial.SetStep(TutorialStep.MoveRight);
        }

        public void UpdateState(TutorialManager tutorial)
        {
            if (platformScript != null && platformScript.IsSteppedOn())
            {
                platformScript.ResetPlatform();
                tutorial.SetState(new Tutorial1_TalkWithNPCState(TutorialStep.MoveLeft));
            }
        }
    }
}

