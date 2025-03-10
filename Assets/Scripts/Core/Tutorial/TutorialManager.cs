using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Core.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialState currentState;
        private bool isNPCInteracted = false; 
        public TextMeshProUGUI dialogueText;
        public GameObject dialoguePanel;


        public static TutorialManager instance { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject); // 중복 방지
            }
        }

        // Start is called before the first frame update
        void Start()
        {

            StartTutorial(new Tutorial1State());
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState(this);
            }
        }

        public void StartTutorial(TutorialState tutorialState)
        {
            SetState(tutorialState);
        }

        public void SetState(TutorialState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        public void ShowDialogue(string text)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = text;
        }

        public void NPCInteracted()
        {
            isNPCInteracted = true;
        }

        public bool HasNPCInteracted()
        {
            return isNPCInteracted;
        }

    }
}



