using Core.Unit.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Camera
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance { get; private set; }

        public Transform player;   // 플레이어 참조
        public Vector3 offset = new Vector3(0, 5, -6); // 카메라와 플레이어 간의 거리
        public float smoothSpeed = 5f; // 부드러운 이동 속도


        private bool isPlayerMissing = false;

        private void Awake()
        {
            // 싱글톤 설정 (중복 생성 방지)
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

        }

        private void Start()
        {
            FindPlayer(); 
        }


        void LateUpdate()
        {
            if (player == null)
                return;

            // 목표 위치 계산 (플레이어 위치 + 오프셋)
            Vector3 targetPosition = player.position + offset;

            // 부드럽게 따라가기
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }

        private void Update()
        {
            if (isPlayerMissing)
            {
                FindPlayer();
            }
        }


        private void FindPlayer()
        {
            GameObject playerObj = GameObject.FindWithTag("Player"); // "Player" 태그 사용
            if (playerObj != null)
            {
                player = playerObj.transform;
                isPlayerMissing = false; // 플레이어 찾으면 탐색 중지
            }
        }
    }
}


