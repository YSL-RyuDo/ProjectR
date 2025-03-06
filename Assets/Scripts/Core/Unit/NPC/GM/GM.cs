using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Unit.NPC.GM
{
    public class GM : MonoBehaviour
    {
        public float interactionRadius = 2.0f;  // ���� �ݰ�
        private GameObject player;  // �÷��̾� ��ü

        private void Start()
        {
            // �±׸� �̿��� �÷��̾� ã��
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (player == null) return;

            // �÷��̾�� GM�� �Ÿ� üũ
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= interactionRadius)
            {
                Debug.Log("�÷��̾ GM ��ó�� ����!");

                // �÷��̾ ��ȣ�ۿ� Ű�� ������ ����
                if (Manager.InputManager.InputManager.instance.interaction)
                {
                    Debug.Log("GM�� �÷��̾�� ��ȣ�ۿ�!");
                }
            }
        }
    }

}
