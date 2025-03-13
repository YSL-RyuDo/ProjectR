using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manager.InputManager;

namespace Core.UI.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialoguePanel;

        public Image characterImage;

        public TMP_Text dialogueText;
        public TMP_Text nameText;

        public static DialogueManager instance { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                dialoguePanel.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if(InputManager.instance.escButton)
            {
                ESCButtonClick();
            }
        }

        public void ShowDialogue(string dialogue, string name)
        {
            dialogueText.text = dialogue;
            nameText.text = name;

            dialoguePanel.SetActive(true);
        }

        public void NextButtonClick()
        {

        }

        public void ESCButtonClick()
        {
            dialoguePanel.SetActive(false);
        }

    }
}
