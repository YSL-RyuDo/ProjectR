using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력 매니저, 입력을 감지하고 저장함
namespace Manager.InputManager
{
    public class InputManager : MonoBehaviour
    {
        //싱글톤 패턴 적용, 읽기 전용 프로퍼티로 설정
        //싱글톤 패턴으로 다른 스크립트에서 네임스페이스 설정 후 InputManager.instance로 접근 가능
        public static InputManager instance { get; private set; }

        //수평 이동 정보
        public float horizontal {  get; private set; }

        //수직 이동 정보
        public float vertical { get; private set; } 

        //점프
        public bool jump {  get; private set; }

        //지상에서 벽 붙기
        public bool wallAttach { get; private set; }

        //달리기
        public bool sprint {  get; private set; }


        private void Awake()
        {
            //인스턴스로 처음 실행될 때 자기 자신만 존재하도록 함
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

