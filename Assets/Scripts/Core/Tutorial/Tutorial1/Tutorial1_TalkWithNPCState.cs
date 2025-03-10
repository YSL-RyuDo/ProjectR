using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_TalkWithNPCState : TutorialState
    {
        public void EnterState(TutorialManager tutorial)
        {
            tutorial.ShowDialogue("Good");
        }

        public void UpdateState(TutorialManager tutorial)
        {
            tutorial.SetState(new Tutorial1_MoveRightState());
        }
    }
}


