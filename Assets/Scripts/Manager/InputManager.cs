using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Է� �Ŵ���, �Է��� �����ϰ� ������
namespace Manager.InputManager
{
    public class InputManager : MonoBehaviour
    {
        //�̱��� ���� ����, �б� ���� ������Ƽ�� ����
        //�̱��� �������� �ٸ� ��ũ��Ʈ���� ���ӽ����̽� ���� �� InputManager.instance�� ���� ����
        public static InputManager instance { get; private set; }

        //���� �̵� ����
        public float horizontal {  get; private set; }

        //���� �̵� ����
        public float vertical { get; private set; } 

        //����
        public bool jump {  get; private set; }

        //���󿡼� �� �ٱ�
        public bool wallAttach { get; private set; }

        //�޸���
        public bool sprint {  get; private set; }


        private void Awake()
        {
            //�ν��Ͻ��� ó�� ����� �� �ڱ� �ڽŸ� �����ϵ��� ��
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            jump = Input.GetKey(KeyCode.LeftAlt);
            wallAttach = Input.GetKey(KeyCode.C);
            sprint = Input.GetKey(KeyCode.LeftShift);

        }
    }
}

