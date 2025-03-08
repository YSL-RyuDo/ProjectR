using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Manager.InputManager;

namespace Core.Unit.Player
{
    public class PlayerController : MonoBehaviour
    {
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

        private Animator animator;
        private CharacterController controller;

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
            float wallRayLength = 0.3f; // 감지 거리 조정
            float sphereRadius = 0.2f;  // 감지 반경 확장
            float wallAttachThreshold = 0.2f; // 벽 근처 감지 거리 (이보다 가까우면 자동으로 붙지 않음)

            Vector3 rayStart = transform.position + Vector3.up * 0.25f;
            Debug.DrawRay(rayStart, transform.forward * wallRayLength, Color.red, 0.1f);

            // SphereCast를 사용하여 감지 성능 향상
            if (Physics.SphereCast(rayStart, sphereRadius, transform.forward, out hit, wallRayLength))
            {
                if (hit.collider.CompareTag("Ground")) // 벽이 Ground 태그를 가질 때만 적용
                {  
                    wallNormal = hit.normal;

                    float distanceToWall = Vector3.Distance(transform.position, hit.point); // 벽과의 거리 측정

                    // 플레이어가 벽과 가까우면 벽붙기 입력이 필요
                    if (!isGrounded && isJumped && distanceToWall > wallAttachThreshold)
                    {
                        isTouchingWall = true;
                        isWallHanging = true;
                        velocity.y = 0;
                        animator.SetBool("WallHanging", true);
                    }
                    else if (isGrounded && InputManager.instance.wallAttach)
                    {
                        isTouchingWall = true;
                        // 벽 근처에서는 wallAttach 입력이 있어야만 벽에 붙을 수 있음
                        isWallHanging = true;
                        velocity = Vector3.zero;
                        animator.SetBool("WallHanging", true);
                        controller.Move(Vector3.up * 0.2f); // 플레이어를 살짝 올려줌
                    }

                    lastSafePosition = transform.position;  // 벽에서 매달려도 안전한 위치 갱신
                    return;
                }
            }

            // 벽 감지가 실패하면 isTouchingWall을 false로 설정
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

            if (isWallHanging && InputManager.instance.jump)
            {
                isWallHanging = false;
                isTouchingWall = false;
                animator.SetTrigger("WallJump");
                animator.SetBool("WallHanging", false);

                // 플레이어가 보고 있는 방향의 반대 방향으로 점프
                Vector3 jumpDirection = -transform.forward; // 현재 바라보는 방향의 반대 방향
                controller.Move(jumpDirection * 0.5f); // 살짝 밀어내는 효과 추가

                // 기존 점프력 유지
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }


        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
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