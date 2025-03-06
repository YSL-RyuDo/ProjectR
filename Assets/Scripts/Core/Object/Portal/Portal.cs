using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public string targetSceneName; // �̵��� �� �̸�
    public float interactionRadius = 2.0f;  // ���� �ݰ� (��Ż�� �÷��̾� �� �Ÿ�)

    private GameObject player;  // �÷��̾� ������Ʈ

    private void Start()
    {
        // �±׸� �̿��� �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null) return;

        // �÷��̾�� ��Ż�� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionRadius)
        {       
            // �÷��̾ ��ȣ�ۿ� Ű�� ������ �� ��ȯ
            if (Manager.InputManager.InputManager.instance.interaction)
            {
                Debug.Log("��Ż");
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }
}
