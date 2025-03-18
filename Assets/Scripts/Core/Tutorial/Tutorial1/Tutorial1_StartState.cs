using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_StartState : TutorialState
    {
        public void EnterState(TutorialManager tutorial)
        {
            Debug.Log("튜토리얼 시작, GM에게 말 걸기");
        }

        public void UpdateState(TutorialManager tutorial)
        {
            if (tutorial.HasNPCInteracted())
            {
                tutorial.SetState(new Tutorial1_TalkWithNPCState(TutorialStep.MoveRight));
                tutorial.ResetNPCInteraction();
            }
        }
    }
}

