using Core.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public class Tutorial1_MoveRightState : TutorialState
    {
        private GameObject targetBlock;

        private GameObject highlightEffect;

        private Vector3 targetPosition = new Vector3(3, 0, 0); // 이동 목표 위치
        private bool hasReachedTarget = false;

        public void EnterState(TutorialManager tutorial)
        {
            tutorial.ShowDialogue("Right Move Please");

            // 이동할 블록을 찾고 하이라이트 적용
            targetBlock = GameObject.Find("RightTargetBlock");
            if (targetBlock != null)
            {
            }
        }

        public void UpdateState(TutorialManager tutorial)
        {

        }

        public void OnPlayerReachedTarget()
        {
            // 상태 변경 전에 하이라이트 이펙트 제거
            if (highlightEffect != null)
            {
                GameObject.Destroy(highlightEffect);
            }

            // 다음 상태로 변경
            TutorialManager.instance.SetState(new Tutorial1_TalkWithNPCState());
        }

        private void CreateHighlightEffect(Vector3 position)
        {
            // Resources 폴더에서 이펙트 프리팹 불러오기
            GameObject effectPrefab = Resources.Load<GameObject>("Assets/Resources/Prefabs/TargetEffect.prefab");
            if (effectPrefab != null)
            {
                // 이펙트 생성
                highlightEffect = GameObject.Instantiate(effectPrefab, position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("HighlightEffect 프리팹을 찾을 수 없습니다! 경로 확인 필요");
            }
        }

    }
}

