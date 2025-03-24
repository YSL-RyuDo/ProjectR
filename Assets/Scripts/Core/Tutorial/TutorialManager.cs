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

        public GameObject[] portals;

        private Queue<GameObject> platformPool = new Queue<GameObject>(); 

        public GameObject platformPrefab; 

        private bool tutorial1Completed = false; 

        public static TutorialManager instance { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        void Start()
        {
            if (!tutorial1Completed) 
            {
                StartTutorial(new Tutorial1_StartState());
            }
        }

        void Update()
        {
            currentState?.UpdateState(this);

        }

        // 튜토리얼 시작
        public void StartTutorial(TutorialState tutorialState)
        {
            SetState(tutorialState);
        }

        // 상태 변환
        public void SetState(TutorialState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }


        // NPC 상호작용 여부 
        public void NPCInteracted()
        {
            isNPCInteracted = true;
        }

        // NPC 상호작용 여부 확인
        public bool HasNPCInteracted()
        {
            return isNPCInteracted;
        }

        // NPC 상호작용 여부 초기화
        public void ResetNPCInteraction()
        {
            isNPCInteracted = false;
        }

        // 오브젝트 풀에서 발판 가져오기 
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

        // 튜토리얼 완료 처리
        public void CompleteTutorial(int tutorialNumber)
        {
            if (tutorialNumber == 1)
            {
                tutorial1Completed = true;
            }
        }
    }
}