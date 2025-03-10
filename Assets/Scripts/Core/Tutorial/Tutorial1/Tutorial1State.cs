using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactable.NPC.GM;

namespace Core.Tutorial
{
    public class Tutorial1State : TutorialState
    {
        public void EnterState(TutorialManager tutorial)
        {
            tutorial.ShowDialogue("Tutorial 1 Start, Talk with NPC.");
        }

        public void UpdateState(TutorialManager tutorial)
        {
            if (tutorial.HasNPCInteracted())
            {
                tutorial.SetState(new Tutorial1_MoveRightState()); // 다음 상태로 변경
            }
        }
    }
}



