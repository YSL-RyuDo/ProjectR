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

        public Transform player;  
        public Vector3 offset = new Vector3(0, 5, -6); 
        public float smoothSpeed = 5f; 


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
            if (player == null) return;
 
            Vector3 targetPosition = player.position + offset;

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
            GameObject playerObj = GameObject.FindWithTag("Player"); 
            if (playerObj != null)
            {
                player = playerObj.transform;
                isPlayerMissing = false;
            }
        }
    }
}


