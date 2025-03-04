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
        public float jumpHeight = 2f;
        public float gravity = -9.81f;
        public float wallHangGravity = 0f;



        public float rotationSpeed = 10f;

        private Vector3 velocity;
        private Vector3 wallNormal;


        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool isWallHanging = false;
        [SerializeField]
        private bool isTouchingWall;


        Animator animator;

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
                HandleWallHang();
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

                controller.Move(move * moveSpeed * Time.deltaTime);
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

            float wallRayLength = 1f;

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
                }
            }
            else
            {
                isTouchingWall = false;
            }
        }

        void HandleWallHang()
        {
            // 벽 매달릴 때는 중력 적용 안함
            velocity.y = wallHangGravity;
            controller.Move(Vector3.zero); // 움직이지 않도록 고정

            // 플레이어가 방향키를 반대 방향으로 누르면 매달리기 해제
            if (InputManager.instance.horizontal * wallNormal.x > 0)
            {
                isWallHanging = false;
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



