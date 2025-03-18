using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.InputManager;

namespace Core.UI.Button
{
    public class ButtonManager : MonoBehaviour
    {
        public GameObject Panel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ESCButtonOnClick()
        {
            if(InputManager.instance.escButton)
            {
                Panel.SetActive(false);
            }
            Panel.SetActive(false);
        }

        public void NextButtonClick()
        {
            if(InputManager.instance.spaceButton)
            {
                Panel.SetActive(false);
            }
            Panel.SetActive(false);
        }
    }
}

