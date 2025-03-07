using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Manager.InputManager;
using Core.Interactable;

namespace Core.Unit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float interactionRange = 2.0f;

        public LayerMask interactableLayer;

        private GameObject currentInteractable = null;

        void FixedUpdate() // 최적화를 위해 FixedUpdate 사용
        {
            DetectClosestInteractable();
        }

        void Update()
        {
            if (currentInteractable != null && InputManager.instance.interaction)
            {
                InteractWithObject();
            }
        }

        // 가장 가까운 상호작용 오브젝트를 구별하여 상호작용하도록 함
        void DetectClosestInteractable()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);

            if (hitColliders.Length > 0)
            {
                GameObject closestObject = null;
                float minDistance = Mathf.Infinity;

                foreach (Collider col in hitColliders)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestObject = col.gameObject;
                    }
                }

                if (closestObject != currentInteractable)
                {
                    currentInteractable = closestObject;
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable = null;
                }
            }
        }

        // 현재 감지된 오브젝트와 상호작용
        void InteractWithObject()
        {
            if (currentInteractable != null)
            {
                Core.Interactable.Interactable interactable = currentInteractable.GetComponent<Core.Interactable.Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
                else
                {
                    Debug.Log("Interactable이 없음");
                }
            }
        }

        // 감지 범위 시각화 (에디터에서 확인 가능)
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
    }



}

