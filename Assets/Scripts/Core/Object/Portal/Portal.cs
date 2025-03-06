using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public string targetSceneName; // 이동할 씬 이름
    public float interactionRadius = 2.0f;  // 감지 반경 (포탈과 플레이어 간 거리)

    private GameObject player;  // 플레이어 오브젝트

    private void Start()
    {
        // 태그를 이용해 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null) return;

        // 플레이어와 포탈의 거리 계산
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionRadius)
        {       
            // 플레이어가 상호작용 키를 누르면 씬 전환
            if (Manager.InputManager.InputManager.instance.interaction)
            {
                Debug.Log("포탈");
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }
}
