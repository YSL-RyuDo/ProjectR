using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class TargetBlock : MonoBehaviour
    {
        private bool isTriggered = false;
        private TutorialState targetState; // 트리거할 상태 저장

        public void SetTargetState(TutorialState state)
        {
            targetState = state; // 블록이 트리거할 상태 설정
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isTriggered && other.CompareTag("Player")) // 플레이어가 닿으면 실행
            {
                isTriggered = true;
                if (targetState != null)
                {
                    TutorialManager.instance.SetState(targetState); // 상태 변경
                }
            }
        }

    }
}

