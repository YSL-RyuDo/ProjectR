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

            CheckFall();  // 낙하 체크 추가
        }

        void HandleMovement()
        {
            float speed = InputManager.instance.sprint ? moveSpeed * 1.5f : moveSpeed;
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
            if (isGrounded && InputManager.instance.jump)
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
            float wallRayLength = 0.5f;
            Vector3 rayStart = transform.position;
            Debug.DrawRay(rayStart, transform.forward * wallRayLength, Color.red, 0.1f);

            if (Physics.Raycast(rayStart, transform.forward, out hit, wallRayLength))
            {
                isTouchingWall = true;
                wallNormal = hit.normal;

                if (!isGrounded && isJumped)
                {
                    isWallHanging = true;
                    velocity.y = 0;
                    animator.SetBool("WallHanging", true);
                }

                if (isGrounded && InputManager.instance.wallAttach)
                {
                    isWallHanging = true;
                    velocity = Vector3.zero;
                    animator.SetBool("WallHanging", true);
                }

                lastSafePosition = transform.position;  // 벽에서 매달려도 안전한 위치 갱신
            }
            else
            {
                isTouchingWall = false;
                animator.SetBool("WallHanging", false);
            }
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

            if (InputManager.instance.jump)
            {
                isWallHanging = false;
                animator.SetTrigger("WallJump");
            }

            if (!isTouchingWall)
            {
                animator.SetBool("WallHanging", false);
                isWallHanging = false;
            }
        }

        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        // ✅ 플레이어가 떨어지면 마지막 안전한 위치로 복귀
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