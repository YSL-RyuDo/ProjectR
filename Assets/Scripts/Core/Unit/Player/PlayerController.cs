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

        public float fallThreshold = -15f; 

        private float sprintTimer = 0f;
        private float sprintCooldownTimer = 0f;
        private float sprintDuration = 3f;
        private float sprintCooldown = 5f;

        private Vector3 velocity;
        private Vector3 wallNormal;
        private Vector3 lastSafePosition; 

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
            lastSafePosition = transform.position;  
        }

        void Update()
        {
            // 점프했거나 벽붙기 입력이 있을 때만 벽 체크
            if (isJumped || InputManager.instance.wallAttach)
            {
                HandleWallCheck(); 
            }

            HandleSprint();
            HandleMovement();
            HandleJump();
            ApplyGravity();
        }

        private void FixedUpdate()
        {
            HandleGroundCheck();
            CheckFall(); 
        }

        // 달리기 관리하는 함수
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


        // 기본 이동 관리 함수
        void HandleMovement()
        {
          
            if (isWallHanging)
            {
                HandleWallCheck();
                HandleWallHangMovement();

                return;
            }

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

        // 점프 관리 함수
        void HandleJump()
        {
            if (isGrounded && InputManager.instance.jump && !isTouchingWall)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumped = true;
                HandleWallCheck();
                animator.SetTrigger("Jump");
            }

            if(velocity.y > 0)
            {
                isJumpingUp = true;
            }
        }


        // 지면 체크 함수
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

                animator.SetBool("WallHanging", false);
                animator.SetFloat("MoveSpeed", 0f);

                lastSafePosition = transform.position; 
            }
            else
            {
                isGrounded = false;
            }
        }


        // 벽 체크 함수
        void HandleWallCheck()
        {
            RaycastHit hit;
            float wallRayLength = 0.4f; 

            Vector3 rayStart = transform.position;
            Debug.DrawRay(rayStart, transform.forward * wallRayLength, Color.red, 0.1f);

            if (Physics.Raycast(rayStart, transform.forward, out hit, wallRayLength))
            {
                if (hit.collider.CompareTag("Ground")) 
                {
                    wallNormal = hit.normal;                  

                   
                    bool isMoving = InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0;
  
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
                        controller.Move(Vector3.up * 0.2f); 
                    }

                    lastSafePosition = transform.position;
                    return;
                }
            }

            isTouchingWall = false;
            animator.SetBool("WallHanging", false);
        }

        // 벽 매달렸을 때 이동 관리 함수
        void HandleWallHangMovement()
        {
            velocity.y = wallHangGravity;
            controller.Move(Vector3.zero);
            isJumped = false;

            float verticalMove = InputManager.instance.vertical * wallClimbSpeed * Time.deltaTime;
            float horizontalMove = InputManager.instance.horizontal * wallSideMoveSpeed * Time.deltaTime;

            Vector3 wallMove = (Vector3.up * verticalMove) + (transform.right * horizontalMove);
            controller.Move(wallMove);

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

        // 중력 함수
        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (velocity.y <= 0)
            {
                isJumpingUp = false; 
            }
        }


        // 낙하 관리 함수
        void CheckFall()
        {
            if (transform.position.y < fallThreshold)
            {
                Respawn();
            }
        }

        // 리스폰 함수
        void Respawn()
        {
            controller.enabled = false;  
            transform.position = lastSafePosition;
            velocity = Vector3.zero;
            controller.enabled = true; 
            Debug.Log("플레이어가 떨어져서 마지막 안전한 위치로 복귀");
        }
    }
}