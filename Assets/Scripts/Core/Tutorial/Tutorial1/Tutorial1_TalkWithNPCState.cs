using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    // 튜토리얼의 진행 단계
    public enum TutorialStep
    {
        MoveRight,
        MoveLeft,
        MoveToPortal,
        EndTutorial
    }

    public class Tutorial1_TalkWithNPCState : TutorialState
    {
        // 현재 튜토리얼의 진행 단계에 따라 대사 출력 변경
        private static readonly Dictionary<TutorialStep, string> dialogues = new()
        {
            { TutorialStep.MoveRight, "Right" },
            { TutorialStep.MoveLeft, "Left" },
            { TutorialStep.MoveToPortal, "Portal" },
            { TutorialStep.EndTutorial, "Tutorial2" }
        };

        private readonly TutorialStep step;

        public Tutorial1_TalkWithNPCState(TutorialStep step)
        {
            this.step = step;
        }

        public void EnterState(TutorialManager tutorial)
        {
            Debug.Log("GM과 대화");
            tutorial.ShowDialogue(dialogues[step]);
        }

        public void UpdateState(TutorialManager tutorial)
        {
            tutorial.ResetNPCInteraction();
            tutorial.SetState(step switch
            {
                TutorialStep.MoveRight => new Tutorial1_MoveRightState(),
                TutorialStep.MoveLeft => new Tutorial1_MoveLeftState(),
                TutorialStep.MoveToPortal => new Tutorial1_MoveToPortalState(),
                TutorialStep.EndTutorial => new Tutorial1_EndState(),
                _ => null
            });
        }
    }
}


