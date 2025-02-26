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
        public float wallDetachCheckDistance = 0.5f; // 벽 끝 감지 거리
        public float climbOverThreshold = 0.2f; // 꼭대기 감지 거리 (작을수록 민감하게 처리)

        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool isWallAttached; //벽에 붙었는지
        [SerializeField]
        private bool canAttachToWall = false; //벽에 붙을 수 있는지 확인
        [SerializeField]
        private bool tryAttachToWall = false;


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


            // 벽과 충돌한 상태에서 C 키를 누르면 벽에 붙기
            if (canAttachToWall && InputManager.instance.wallAttach)
            {             
                AttachToWall();
            }
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
                if (isGrounded && !tryAttachToWall)
                {
                    isWallAttached = false;
                    rigid.useGravity = true;
                }

                // 벽의 끝을 확인하고 떨어지게 만들기
                if (!CheckWallAhead())
                {
                    if (CheckWallTop()) // 벽 꼭대기인지 확인
                    {
                        ClimbOverWall();
                    }
                    else
                    {
                        DetachFromWall();
                    }
                }


                return; // 벽에 붙어있을 때는 기존 이동 로직 실행 안 함
            }

            // 일반 이동 로직
            rigid.velocity = new Vector3(moveX * moveSpeed, rigid.velocity.y, moveZ * moveSpeed);

            if (jump && isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }
        
        // 지상 벽 붙기 함수 
        private void AttachToWall()
        {
            transform.position += Vector3.up * (climbOverThreshold * 3f);
            tryAttachToWall = true;
            isWallAttached = true;
            rigid.velocity = Vector3.zero;
            rigid.useGravity = false;
        }

        // 벽 확인 함수
        private bool CheckWallAhead()
        {
            return Physics.Raycast(transform.position, -wallNormal, wallDetachCheckDistance);
        }

        // 벽에서 떨어지는 함수
        private void DetachFromWall()
        {
            isWallAttached = false;
            rigid.useGravity = true;
        }

        // 벽 꼭대기 확인 함수
        private bool CheckWallTop()
        {
            return !Physics.Raycast(transform.position + Vector3.up * climbOverThreshold, -wallNormal, wallDetachCheckDistance);
        }

        // 벽 꼭대기 올라가는 함수
        private void ClimbOverWall()
        {
            transform.position += Vector3.up * (climbOverThreshold * 3f) + wallNormal * -0.5f;
            isWallAttached = false;
            rigid.useGravity = true;
            isGrounded = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                Vector3 normal = collision.contacts[0].normal;

                // 바닥인지 확인 (y 값이 충분히 크면 바닥)
                // wall 태그를 따로 안하는 이유는 게임 내에서 맵이 수직적 구조면 이를 올라갈 때 wall 콜라이더를 따로 넣는거보단 이게 낫지 않을까 하는 느낌
                if (normal.y > 0.5f)
                {
                    isGrounded = true;
                    isWallAttached = false; // 벽에서 내려오면 벽 붙기 상태 해제
                    rigid.useGravity = true; // 중력 다시 활성화
                }
                else if (!isGrounded && Mathf.Abs(normal.y) < 0.3f)
                {
                    // 점프 중
                    // 바닥이 아닌 경우 (벽이면)
                    isWallAttached = true;
                    wallNormal = normal; // 벽 방향 저장
                    rigid.velocity = Vector3.zero; // 속도 초기화
                    rigid.useGravity = false; // 중력 제거 (벽에 붙음)    
                }
                else if(Mathf.Abs(normal.y) < 0.3f)
                {
                    // 그냥 이동 중
                    // C 키를 눌러야 벽에 붙을 수 있도록 설정
                    canAttachToWall = true;
                    wallNormal = normal;
                }
            }          
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                canAttachToWall = false; // 벽과 떨어지면 벽 붙기 비활성화
            }
        }
    }
}



