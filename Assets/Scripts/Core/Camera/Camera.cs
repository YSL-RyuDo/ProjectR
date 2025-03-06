using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Camera
{
    public class Camera : MonoBehaviour
    {
        public Transform player;   // �÷��̾� ����
        public Vector3 offset = new Vector3(0, 3, -10); // ī�޶�� �÷��̾� ���� �Ÿ�
        public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�

        void LateUpdate()
        {
            if (player == null)
                return;

            // ��ǥ ��ġ ��� (�÷��̾� ��ġ + ������)
            Vector3 targetPosition = player.position + offset;

            // �ε巴�� ���󰡱�
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}


