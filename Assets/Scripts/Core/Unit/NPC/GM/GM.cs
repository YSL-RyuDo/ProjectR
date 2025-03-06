using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Unit.NPC.GM
{
    public class GM : MonoBehaviour
    {
        public float interactionRadius = 2.0f;  // 감지 반경
        private GameObject player;  // 플레이어 객체

        private void Start()
        {
            // 태그를 이용해 플레이어 찾기
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (player == null) return;

            // 플레이어와 GM의 거리 체크
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= interactionRadius)
            {
                Debug.Log("플레이어가 GM 근처에 도착!");

                // 플레이어가 상호작용 키를 누르면 실행
                if (Manager.InputManager.InputManager.instance.interaction)
                {
                    Debug.Log("GM이 플레이어와 상호작용!");
                }
            }
        }
    }

}
