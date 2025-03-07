using Core.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Interactable.NPC.GM
{
    public class GM : MonoBehaviour, Interactable
    {

        public void Interact()
        {
            Debug.Log("GM이 플레이어와 상호작용!");
        }
    }

}
