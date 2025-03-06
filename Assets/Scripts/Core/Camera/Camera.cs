using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Camera
{
    public class Camera : MonoBehaviour
    {
        public Transform player;   // 플레이어 참조
        public Vector3 offset = new Vector3(0, 3, -10); // 카메라와 플레이어 간의 거리
        public float smoothSpeed = 5f; // 부드러운 이동 속도

        void LateUpdate()
        {
            if (player == null)
                return;

            // 목표 위치 계산 (플레이어 위치 + 오프셋)
            Vector3 targetPosition = player.position + offset;

            // 부드럽게 따라가기
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}


