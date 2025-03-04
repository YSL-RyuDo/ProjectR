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

        public float rotationSpeed = 10f;

        private Vector3 velocity;


        [SerializeField]
        private bool isGrounded;

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
            HandleMovement();
            HandleJump();
            HandleGroundCheck();
            ApplyGravity();
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
        }

        //중력 적용, CharacterController는 rigidbody와 다르게 자동 중력 적용이 아님
        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime; // 지속적으로 중력 적용
            controller.Move(velocity * Time.deltaTime);
        }
    }
}



