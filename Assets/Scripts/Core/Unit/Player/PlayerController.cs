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
        private Rigidbody rigid;

        public float moveSpeed = 5f;
        public float jumpForce = 15f; // ���� ������Ʈ ���ÿ��� �߷� -90���� ������, ����Ƽ �������� 25�� ����
        public float wallMoveSpeed = 3f;
        public float wallDetachCheckDistance = 0.5f; // �� �� ���� �Ÿ�
        public float climbOverThreshold = 0.2f; // ����� ���� �Ÿ� (�������� �ΰ��ϰ� ó��)

        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool isWallAttached; //���� �پ�����
        [SerializeField]
        private bool canAttachToWall = false; //���� ���� �� �ִ��� Ȯ��
        [SerializeField]
        private bool tryAttachToWall = false;

        private Vector3 wallNormal; //���� ����

     
        public enum playerState
        {
            Move,
            Jump,
            Climb
        }
        public static playerState state;


        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();  
        }

        // Update is called once per frame
        void Update()
        {

            HandleMovement();


            // ���� �浹�� ���¿��� C Ű�� ������ ���� �ٱ�
            if (canAttachToWall && InputManager.instance.wallAttach && isGrounded)
            {             
                AttachToWall();
            }
        }

        void HandleMovement()
        {
            float moveX = InputManager.instance.horizontal; // �¿� �Է�
            float moveZ = InputManager.instance.vertical;   // �յ� �Է� (������ ���Ʒ��� ��ȯ)
            bool jump = InputManager.instance.jump;

            // ���� ���� ���� �̵��� ����
            if (state == playerState.Jump)
            {
                return;
            }

            // ���� �پ��ִ� ���
            if (isWallAttached)
            {
                state = playerState.Climb;

                rigid.useGravity = false; // �߷� ����

                //�Է� ��ȯ
                Vector3 moveDirection = new Vector3(moveX, moveZ, 0).normalized * wallMoveSpeed;

                // ���� ������ ���� �̵� ���� ����
                moveDirection = Vector3.ProjectOnPlane(new Vector3(moveX, moveZ, 0), wallNormal);

                rigid.velocity = moveDirection;

                if (jump)
                {
                   
                    DetachFromWall(); // ������ ��������
                    state = playerState.Jump;
                    return;
                }

                // ���� Ÿ�� �ö󰡴ٰ� �ٴڿ� ������ ���� ���·� ����
                if (isGrounded && !tryAttachToWall)
                {

                    isWallAttached = false;
                    rigid.useGravity = true;
                   
                }

                // ���� ���� Ȯ���ϰ� �������� �����
                if (!CheckWallAhead())
                {
                    if (CheckWallTop()) // �� ��������� Ȯ��
                    {
                        ClimbOverWall();
                    }
                    else
                    {
                        DetachFromWall();
                    }
                }


                return; // ���� �پ����� ���� ���� �̵� ���� ���� �� ��
            }

            state = playerState.Move;

            // �Ϲ� �̵� ����
            rigid.velocity = new Vector3(moveX * moveSpeed, rigid.velocity.y, moveZ * moveSpeed);

            if (jump && isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                state = playerState.Jump;   
            }

        }
        
        // ���� �� �ٱ� �Լ� 
        private void AttachToWall()
        {
            //�ڿ������� ���̱� ���� �÷��̾ ��¦ ���� �ø�
            transform.position += Vector3.up * (climbOverThreshold * 1.5f);


            tryAttachToWall = true;
            isWallAttached = true;
            rigid.velocity = Vector3.zero;
            rigid.useGravity = false;
        }

        // �� Ȯ�� �Լ�
        private bool CheckWallAhead()
        {
            return Physics.Raycast(transform.position, -wallNormal, wallDetachCheckDistance);
        }

        // ������ �������� �Լ�
        private void DetachFromWall()
        {
            Vector3 wallJumpDirection = wallNormal * 15f; // ������ �־����� ����
            rigid.velocity = Vector3.zero; // ���� �ӵ� �ʱ�ȭ
            rigid.AddForce(wallJumpDirection, ForceMode.Impulse); // �� ���ϱ�
            isWallAttached = false;
            rigid.useGravity = true;
            state = playerState.Jump; // ���� ���·� ����
        }

        // �� ����� Ȯ�� �Լ�
        private bool CheckWallTop()
        {
            return !Physics.Raycast(transform.position + Vector3.up * climbOverThreshold, -wallNormal, wallDetachCheckDistance);
        }

        // �� ����� �ö󰡴� �Լ�
        private void ClimbOverWall()
        {
            transform.position += Vector3.up * (climbOverThreshold * 3f) + wallNormal * -0.5f;
            isWallAttached = false;
            rigid.useGravity = true;
            isGrounded = true;


            state = playerState.Move;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {

                //�ݶ��̴��� �浹�� ������ ���� ���� �̿�
                Vector3 normal = collision.contacts[0].normal;

                // �ٴ����� Ȯ�� (y ���� ����� ũ�� �ٴ�)
                // wall �±׸� ���� ���ϴ� ������ ���� ������ ���� ������ ������ �̸� �ö� �� wall �ݶ��̴��� ���� �ִ°ź��� �̰� ���� ������ �ϴ� ����
                if (normal.y > 0.5f)
                {
                    isGrounded = true;
                    isWallAttached = false; // ������ �������� �� �ٱ� ���� ����
                    rigid.useGravity = true; // �߷� �ٽ� Ȱ��ȭ

                    state = playerState.Move; // �̵� �����ϵ��� ���� ����
                }
                else if (!isGrounded && Mathf.Abs(normal.y) < 0.3f)
                {
                    // ���� ��
                    // �ٴ��� �ƴ� ��� (���̸�)
                    isWallAttached = true;
                    wallNormal = normal; // �� ���� ����
                    rigid.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                    rigid.useGravity = false; // �߷� ���� (���� ����)    
                    state = playerState.Climb;
                }
                else if(Mathf.Abs(normal.y) < 0.3f)
                {
                    // �׳� �̵� ��
                    // C Ű�� ������ ���� ���� �� �ֵ��� ����
                    canAttachToWall = true;
                    wallNormal = normal;
                }
            }          
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                canAttachToWall = false; // ���� �������� �� �ٱ� ��Ȱ��ȭ
            }
        }
    }
}



