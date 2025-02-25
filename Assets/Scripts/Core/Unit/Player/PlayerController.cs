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

        [SerializeField]
        private bool isGrounded;
        private bool isWallAttached; //���� �پ�����
        private Vector3 wallNormal; //���� ����

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();  
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            float moveX = InputManager.instance.horizontal; // �¿� �Է�
            float moveZ = InputManager.instance.vertical;   // �յ� �Է� (������ ���Ʒ��� ��ȯ)
            bool jump = InputManager.instance.jump;

            // ���� �پ��ִ� ���
            if (isWallAttached)
            {
                rigid.useGravity = false; // �߷� ����

                //�Է� ��ȯ
                Vector3 moveDirection = new Vector3(moveX, moveZ, 0).normalized * wallMoveSpeed;

                // ���� ������ ���� �̵� ���� ����
                moveDirection = Vector3.ProjectOnPlane(new Vector3(moveX, moveZ, 0), wallNormal);

                rigid.velocity = moveDirection;

                // ���� Ÿ�� �ö󰡴ٰ� �ٴڿ� ������ ���� ���·� ����
                if (isGrounded)
                {
                    isWallAttached = false;
                    rigid.useGravity = true;
                }

                return; // ���� �پ����� ���� ���� �̵� ���� ���� �� ��
            }

            // �Ϲ� �̵� ����
            Vector3 normalMove = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;
            rigid.velocity = new Vector3(normalMove.x, rigid.velocity.y, normalMove.z);

            if (jump && isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                Vector3 normal = collision.contacts[0].normal;

                // �ٴ����� Ȯ�� (y ���� ����� ũ�� �ٴ�)
                if (normal.y > 0.5f)
                {
                    isGrounded = true;
                    isWallAttached = false; // ������ �������� �� �ٱ� ���� ����
                    rigid.useGravity = true; // �߷� �ٽ� Ȱ��ȭ
                }
                else if (!isGrounded && Mathf.Abs(normal.y) < 0.3f)
                {
                    // �ٴ��� �ƴ� ��� (���̸�)
                    isWallAttached = true;
                    wallNormal = normal; // �� ���� ����
                    rigid.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                    rigid.useGravity = false; // �߷� ���� (���� ����)
                }
            }
            

        }
    }
}



