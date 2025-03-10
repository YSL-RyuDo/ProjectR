using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Manager.InputManager;

namespace Core.Unit.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance { get; private set; }


        public float moveSpeed = 5f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;

        public float wallHangGravity = 0f;
        public float wallClimbSpeed = 2f;
        public float wallSideMoveSpeed = 2f;

        public float rotationSpeed = 10f;

        public float fallThreshold = -15f;  // 낙하 감지 높이

        private float sprintTimer = 0f;
        private float sprintCooldownTimer = 0f;
        private float sprintDuration = 3f;
        private float sprintCooldown = 5f;

        private Vector3 velocity;
        private Vector3 wallNormal;
        private Vector3 lastSafePosition; // 마지막 안전한 위치 저장

        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool isJumped;
        [SerializeField]
        private bool isWallHanging = false;
        [SerializeField]
        private bool isTouchingWall;
        
        private bool isSprinting = false;
        private bool canSprint = true;

        private bool isJumpingUp = false;

        private Animator animator;
        private CharacterController controller;

        private void Awake()
        {
            if(instance == null)
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

        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            lastSafePosition = transform.position;  // 초기 안전한 위치 저장
        }

        void Update()
        {
            HandleGroundCheck();
            

            if (isWallHanging)
            {
                HandleWallCheck();
                HandleWallHangMovement();
            }
            else
            {

                if (isJumped || InputManager.instance.wallAttach)
                {
                    HandleWallCheck(); // 점프했거나 벽붙기 입력이 있을 때만 벽 체크
                }

                HandleSprint();
                HandleMovement();
                HandleJump();
                ApplyGravity();
            }

            CheckFall();  // 낙하 체크 추가
        }

        void HandleSprint()
        {
            if(InputManager.instance.sprint && canSprint)
            {
                isSprinting = true;
                canSprint = false;

                sprintTimer = sprintDuration;
            }

            if(isSprinting)
            {
                sprintTimer -= Time.deltaTime;
                if(sprintTimer <= 0f)
                {
                    isSprinting = false;

                    sprintCooldownTimer = sprintCooldown;
                }
            }

            if(!canSprint)
            {
                sprintCooldownTimer -= Time.deltaTime;
                if(sprintCooldownTimer <= 0f)
                {
                    canSprint = true;
                }
            }
        }


        void HandleMovement()
        {
            float speed = isSprinting ? moveSpeed * 1.5f : moveSpeed;
            Vector3 move = new Vector3(InputManager.instance.horizontal, 0, InputManager.instance.vertical).normalized;

            if (move.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                controller.Move(move * speed * Time.deltaTime);
            }

            if (!isWallHanging)
            {
                float moveSpeedValue = move.magnitude * speed;
                animator.SetFloat("MoveSpeed", moveSpeedValue);
            }
        }

        void HandleJump()
        {
            if (isGrounded && InputManager.instance.jump && !isTouchingWall)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumped = true;
                animator.SetTrigger("Jump");
            }

            if(velocity.y > 0)
            {
                isJumpingUp = true;
            }
        }

        void HandleGroundCheck()
        {
            RaycastHit hit;
            float rayLength = 0.3f;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
            {
                isGrounded = true;
                isJumped = false;
                isWallHanging = false;
                isTouchingWall = false;

                animator.SetBool("WallHanging", false); // 벽 매달리기 해제
                animator.SetFloat("MoveSpeed", 0f); // 기본 이동 애니메이션 설정

                lastSafePosition = transform.position;  // 안전한 위치 갱신
            }
            else
            {
                isGrounded = false;
            }
        }

        void HandleWallCheck()
        {
            RaycastHit hit;
            float wallRayLength = 0.4f; // 감지 거리 조정

            Vector3 rayStart = transform.position;
            Debug.DrawRay(rayStart, transform.forward * wallRayLength, Color.red, 0.1f);

            if (Physics.Raycast(rayStart, transform.forward, out hit, wallRayLength))
            {
                if (hit.collider.CompareTag("Ground")) // 벽이 Ground 태그를 가질 때만 적용
                {
                    wallNormal = hit.normal;                  

                    // 방향키 입력이 있을 때만 벽에 붙기
                    bool isMoving = InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0;

                    // 벽에 붙을 수 있는 조건:
                    // 1. 점프 상승 중이 아닐 것 (velocity.y <= 0)
                    // 2. 점프가 끝났을 것 (!isJumped)
                    if (!isGrounded && !isJumpingUp && isMoving)
                    {
                        isTouchingWall = true;
                        isWallHanging = true;
                        velocity.y = 0;
                        animator.SetBool("WallHanging", true);
                    }
                    else if (isGrounded && InputManager.instance.wallAttach && isMoving)
                    {
                        isTouchingWall = true;
                        isWallHanging = true;
                        velocity = Vector3.zero;
                        animator.SetBool("WallHanging", true);
                        controller.Move(Vector3.up * 0.2f); // 플레이어를 살짝 올려줌
                    }

                    lastSafePosition = transform.position;  // 벽에서 매달려도 안전한 위치 갱신
                    return;
                }
            }

            isTouchingWall = false;
            animator.SetBool("WallHanging", false);
        }

        void HandleWallHangMovement()
        {
            velocity.y = wallHangGravity;
            controller.Move(Vector3.zero);
            isJumped = false;

            float verticalMove = InputManager.instance.vertical * wallClimbSpeed * Time.deltaTime;
            float horizontalMove = InputManager.instance.horizontal * wallSideMoveSpeed * Time.deltaTime;

            Vector3 wallMove = (Vector3.up * verticalMove) + (transform.right * horizontalMove);
            controller.Move(wallMove);

            // 벽에서 떨어졌다면 매달리기 상태 해제
            if (!isTouchingWall)
            {
                isWallHanging = false;
                animator.SetBool("WallHanging", false);
            }

            if (isWallHanging && InputManager.instance.wallJump)
            {
                isWallHanging = false;
                isTouchingWall = false;
                animator.SetTrigger("WallJump");
                animator.SetBool("WallHanging", false);
            }
        }


        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (velocity.y <= 0)
            {
                isJumpingUp = false; // 하강 시작
            }
        }

        void CheckFall()
        {
            if (transform.position.y < fallThreshold)
            {
                Respawn();
            }
        }

        void Respawn()
        {
            controller.enabled = false;  // 충돌 방지를 위해 비활성화
            transform.position = lastSafePosition;
            velocity = Vector3.zero;
            controller.enabled = true;  // 다시 활성화
            Debug.Log("플레이어가 떨어져서 마지막 안전한 위치로 복귀");
        }
    }
}