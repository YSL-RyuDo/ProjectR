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
        public float moveSpeed = 5f;
        public float jumpHeight = 2f; //돌려보면서 엔진에서 변동
        public float gravity = -9.81f;

        public float wallHangGravity = 0f; //벽에 매달렸을 땐 중력 적용 안되게
        public float wallClimbSpeed = 2f;   // 벽에서 위/아래 이동 속도
        public float wallSideMoveSpeed = 2f; // 벽에서 좌우 이동 속도


        public float rotationSpeed = 10f;

        private Vector3 velocity; //이동 속도

        private Vector3 wallNormal; //벽 방향 저장


        [SerializeField]
        private bool isGrounded; //지면 확인
        [SerializeField]
        private bool isWallHanging = false; //벽에 매달렸는지 확인
        [SerializeField]
        private bool isTouchingWall; //벽에 닿았는지 확인


        private Animator animator;

        private CharacterController controller;
     

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleGroundCheck();
            HandleWallCheck();

            if (isWallHanging)
            {
                HandleWallHangMovement();
            }
            else
            {
                HandleMovement();
                HandleJump();
                ApplyGravity();
            }
        }

        void HandleMovement()
        {
            //달리기 상태면 이동속도의 150%, 아니면 기본 속도
            float speed = InputManager.instance.sprint ? moveSpeed * 1.5f : moveSpeed;
            Vector3 move = new Vector3(InputManager.instance.horizontal, 0, InputManager.instance.vertical).normalized;

            if(move.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                controller.Move(move * speed * Time.deltaTime);
            }
        }

        void HandleJump()
        {
            if(isGrounded && InputManager.instance.jump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        void HandleGroundCheck()
        {
            RaycastHit hit;
            float rayLength = 0.3f;  // Ray 길이 조정

            isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, rayLength);

            if(isGrounded)
            {
                isWallHanging = false;
            }
        }

        void HandleWallCheck()
        {
            RaycastHit hit;

            float wallRayLength = 0.9f;

            //플레이어 앞에 지형인데 벽처럼 높으면 붙게 함
            if(Physics.Raycast(transform.position, transform.forward, out hit, wallRayLength))
            {
                if (hit.collider.CompareTag("Ground"))  // 벽이 Ground 태그면 매달리기 가능
                {
                    isTouchingWall = true;
                    wallNormal = hit.normal; // 벽 방향 저장

                    if (!isGrounded && velocity.y < 0) // 공중에서 벽에 부딪혔을 때
                    {
                        isWallHanging = true;
                        velocity.y = 0; // 중력 제거
                    }


                    if (isGrounded && InputManager.instance.wallAttach)
                    {
                        isWallHanging = true;
                        velocity = Vector3.zero; // 중력 제거 및 움직임 정지
                    }
                }
            }
            else
            {
                isTouchingWall = false;
            }
        }

        void HandleWallHangMovement()
        {
            // 벽 매달릴 때 중력 제거
            velocity.y = wallHangGravity;
            controller.Move(Vector3.zero); // 기본 이동 방지

            // 위/아래(점프 버튼 & 아래 이동 키) 이동
            float verticalMove = InputManager.instance.vertical * wallClimbSpeed * Time.deltaTime;

            // 좌우 이동 (벽에 매달려서 좌우로 이동 가능)
            float horizontalMove = InputManager.instance.horizontal * wallSideMoveSpeed * Time.deltaTime;

            // 플레이어가 벽을 기준으로 위/아래/좌우 이동 가능하도록 함
            Vector3 wallMove = (Vector3.up * verticalMove) + (transform.right * horizontalMove);
            controller.Move(wallMove);

            // 벽에서 이동할 때 애니메이션 추가 가능 (ex: animator.SetFloat("WallMove", Mathf.Abs(verticalMove) + Mathf.Abs(horizontalMove)))

            // 방향키 반대로 누르면 벽 매달리기 해제
            if (InputManager.instance.horizontal * wallNormal.x > 0)
            {
                isWallHanging = false;
            }

            // ? 이동 후 벽이 있는지 다시 검사 (없으면 떨어짐)
            if (!isTouchingWall)
            {
                isWallHanging = false;
                return;
            }

        }


        //중력 적용, CharacterController는 rigidbody와 다르게 자동 중력 적용이 아님
        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime; // 지속적으로 중력 적용
            controller.Move(velocity * Time.deltaTime);
        }
    }
}



