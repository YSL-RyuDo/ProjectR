using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//InputManage 사용을 위한 네임스페이스
using Manager.InputManager;

namespace Core.Unit.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rigid;

        public float moveSpeed = 5f;
        public float jumpForce = 15f; // 게임 프로젝트 세팅에서 중력 -90으로 설정함, 유니티 엔진에서 25로 설정
        public float wallMoveSpeed = 3f;

        [SerializeField]
        private bool isGrounded;
        private bool isWallAttached; //벽에 붙었는지
        private Vector3 wallNormal; //벽의 방향

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();  
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            float moveX = InputManager.instance.horizontal; // 좌우 입력
            float moveZ = InputManager.instance.vertical;   // 앞뒤 입력 (벽에선 위아래로 변환)
            bool jump = InputManager.instance.jump;

            // 벽에 붙어있는 경우
            if (isWallAttached)
            {
                rigid.useGravity = false; // 중력 제거

                //입력 변환
                Vector3 moveDirection = new Vector3(moveX, moveZ, 0).normalized * wallMoveSpeed;

                // 벽의 방향을 따라 이동 방향 조정
                moveDirection = Vector3.ProjectOnPlane(new Vector3(moveX, moveZ, 0), wallNormal);

                rigid.velocity = moveDirection;

                // 벽을 타고 올라가다가 바닥에 닿으면 원래 상태로 복귀
                if (isGrounded)
                {
                    isWallAttached = false;
                    rigid.useGravity = true;
                }

                return; // 벽에 붙어있을 때는 기존 이동 로직 실행 안 함
            }

            // 일반 이동 로직
            Vector3 normalMove = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;
            rigid.velocity = new Vector3(normalMove.x, rigid.velocity.y, normalMove.z);

            if (jump && isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                Vector3 normal = collision.contacts[0].normal;

                // 바닥인지 확인 (y 값이 충분히 크면 바닥)
                if (normal.y > 0.5f)
                {
                    isGrounded = true;
                    isWallAttached = false; // 벽에서 내려오면 벽 붙기 상태 해제
                    rigid.useGravity = true; // 중력 다시 활성화
                }
                else if (!isGrounded && Mathf.Abs(normal.y) < 0.3f)
                {
                    // 바닥이 아닌 경우 (벽이면)
                    isWallAttached = true;
                    wallNormal = normal; // 벽 방향 저장
                    rigid.velocity = Vector3.zero; // 속도 초기화
                    rigid.useGravity = false; // 중력 제거 (벽에 붙음)
                }
            }
            

        }
    }
}



