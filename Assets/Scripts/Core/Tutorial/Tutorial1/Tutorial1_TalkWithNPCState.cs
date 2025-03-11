using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public enum TutorialStep
    {
        MoveRight,
        MoveLeft,
        MoveToPortal,
        EndTutorial
    }

    public class Tutorial1_TalkWithNPCState : TutorialState
    {
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
            Debug.Log("GM°ú ´ëÈ­");
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


