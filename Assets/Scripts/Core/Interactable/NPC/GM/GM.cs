using Core.Interactable;
using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Interactable.NPC.GM
{
    public class GM : MonoBehaviour, Interactable
    {
        private TutorialManager tutorialManager;

        void Start()
        {
            tutorialManager = TutorialManager.instance;
        }

        public void Interact()
        {
            tutorialManager.NPCInteracted();
            Debug.Log("GM이 플레이어와 상호작용!");
        }

    }

}
