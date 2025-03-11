using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Core.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialState currentState;
        private bool isNPCInteracted = false;
        public TextMeshProUGUI dialogueText;
        public GameObject dialoguePanel;
        private Queue<GameObject> platformPool = new Queue<GameObject>(); // 오브젝트 풀
        public GameObject platformPrefab; // 발판 프리팹

        private const string Tutorial1Key = "Tutorial1Completed"; // 튜토리얼1 완료 여부 저장 키

        public static TutorialManager instance { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // 씬이 넘어가도 유지
            }
            else
            {
                Destroy(gameObject); // 중복 방지
            }
        }

        void Start()
        {
            if (!IsTutorialCompleted(1)) // 튜토리얼1을 완료하지 않았다면 실행
            {
                StartTutorial(new Tutorial1_StartState());
            }
        }

        void Update()
        {
            currentState?.UpdateState(this);
        }

        public void StartTutorial(TutorialState tutorialState)
        {
            SetState(tutorialState);
        }

        public void SetState(TutorialState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        public void ShowDialogue(string text)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = text;
        }

        public void NPCInteracted()
        {
            isNPCInteracted = true;
        }

        public bool HasNPCInteracted()
        {
            return isNPCInteracted;
        }

        public void ResetNPCInteraction()
        {
            isNPCInteracted = false;
        }

        // 오브젝트 풀에서 발판 가져오기 (없으면 새로 생성)
        public GameObject GetPlatform(Vector3 position)
        {
            GameObject platform;
            if (platformPool.Count > 0)
            {
                platform = platformPool.Dequeue();
                platform.transform.position = position;
                platform.SetActive(true);
            }
            else
            {
                platform = Instantiate(platformPrefab, position, Quaternion.identity);
            }
            return platform;
        }

        // 사용한 발판을 다시 풀로 반환
        public void ReturnPlatform(GameObject platform)
        {
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        // 튜토리얼 완료 상태 저장
        public void CompleteTutorial(int tutorialNumber)
        {
            PlayerPrefs.SetInt($"Tutorial{tutorialNumber}Completed", 1);
            PlayerPrefs.Save();
        }

        // 튜토리얼 완료 여부 확인
        public bool IsTutorialCompleted(int tutorialNumber)
        {
            return PlayerPrefs.GetInt($"Tutorial{tutorialNumber}Completed", 0) == 1;
        }
    }
}



