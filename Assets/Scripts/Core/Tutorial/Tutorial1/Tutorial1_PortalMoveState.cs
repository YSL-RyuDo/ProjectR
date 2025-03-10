using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Tutorial
{
    public class Tutorial1_PortalMoveState : TutorialState
    {
        public void EnterState(TutorialManager tutorial)
        {
            tutorial.ShowDialogue("a");
        }

        public void UpdateState(TutorialManager tutorial)
        {

        }
    }
}


