using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_MoveToPortalState : TutorialState
    {
        private GameObject player;

        private GameObject portal;

        public void EnterState(TutorialManager tutorial)
        {
            Debug.Log("Æ÷Å»·Î ÀÌµ¿");
            player = GameObject.FindWithTag("Player");
            portal = tutorial.portals[0];
        }

        public void UpdateState(TutorialManager tutorial)
        {
            if (Vector3.Distance(player.transform.position, portal.transform.position) < 0.7f)
            {
                tutorial.SetState(new Tutorial1_TalkWithNPCState(TutorialStep.EndTutorial));
            }
;
        }
    }
}

