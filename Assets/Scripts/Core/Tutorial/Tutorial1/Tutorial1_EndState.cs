using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_EndState : TutorialState
    {
        public void EnterState(TutorialManager tutorial)
        {
            tutorial.CompleteTutorial(1); // 튜토리얼1 완료 처리
        }

        public void UpdateState(TutorialManager tutorial)
        {
            
        }
    }
}


