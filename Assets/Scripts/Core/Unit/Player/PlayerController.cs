using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//InputManage ����� ���� ���ӽ����̽�
using Manager.InputManager;

namespace Core.Unit.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpHeight = 2f; //�������鼭 �������� ����
        public float gravity = -9.81f;

        public float wallHangGravity = 0f; //���� �Ŵ޷��� �� �߷� ���� �ȵǰ�
        public float wallClimbSpeed = 2f;   // ������ ��/�Ʒ� �̵� �ӵ�
        public float wallSideMoveSpeed = 2f; // ������ �¿� �̵� �ӵ�


        public float rotationSpeed = 10f;

        private Vector3 velocity; //�̵� �ӵ�

        private Vector3 wallNormal; //�� ���� ����


        [SerializeField]
        private bool isGrounded; //���� Ȯ��
        [SerializeField]
        private bool isWallHanging = false; //���� �Ŵ޷ȴ��� Ȯ��
        [SerializeField]
        private bool isTouchingWall; //���� ��Ҵ��� Ȯ��


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
            //�޸��� ���¸� �̵��ӵ��� 150%, �ƴϸ� �⺻ �ӵ�
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
            float rayLength = 0.3f;  // Ray ���� ����

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

            //�÷��̾� �տ� �����ε� ��ó�� ������ �ٰ� ��
            if(Physics.Raycast(transform.position, transform.forward, out hit, wallRayLength))
            {
                if (hit.collider.CompareTag("Ground"))  // ���� Ground �±׸� �Ŵ޸��� ����
                {
                    isTouchingWall = true;
                    wallNormal = hit.normal; // �� ���� ����

                    if (!isGrounded && velocity.y < 0) // ���߿��� ���� �ε����� ��
                    {
                        isWallHanging = true;
                        velocity.y = 0; // �߷� ����
                    }


                    if (isGrounded && InputManager.instance.wallAttach)
                    {
                        isWallHanging = true;
                        velocity = Vector3.zero; // �߷� ���� �� ������ ����
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
            // �� �Ŵ޸� �� �߷� ����
            velocity.y = wallHangGravity;
            controller.Move(Vector3.zero); // �⺻ �̵� ����

            // ��/�Ʒ�(���� ��ư & �Ʒ� �̵� Ű) �̵�
            float verticalMove = InputManager.instance.vertical * wallClimbSpeed * Time.deltaTime;

            // �¿� �̵� (���� �Ŵ޷��� �¿�� �̵� ����)
            float horizontalMove = InputManager.instance.horizontal * wallSideMoveSpeed * Time.deltaTime;

            // �÷��̾ ���� �������� ��/�Ʒ�/�¿� �̵� �����ϵ��� ��
            Vector3 wallMove = (Vector3.up * verticalMove) + (transform.right * horizontalMove);
            controller.Move(wallMove);

            // ������ �̵��� �� �ִϸ��̼� �߰� ���� (ex: animator.SetFloat("WallMove", Mathf.Abs(verticalMove) + Mathf.Abs(horizontalMove)))

            // ����Ű �ݴ�� ������ �� �Ŵ޸��� ����
            if (InputManager.instance.horizontal * wallNormal.x > 0)
            {
                isWallHanging = false;
            }

            // ? �̵� �� ���� �ִ��� �ٽ� �˻� (������ ������)
            if (!isTouchingWall)
            {
                isWallHanging = false;
                return;
            }

        }


        //�߷� ����, CharacterController�� rigidbody�� �ٸ��� �ڵ� �߷� ������ �ƴ�
        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime; // ���������� �߷� ����
            controller.Move(velocity * Time.deltaTime);
        }
    }
}



