using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // 로그인 화면 UI 요소들
    public GameObject LoginView;
    public InputField inputField_ID;
    public InputField inputField_PW;

    // 터치스크린 키보드 변수
    private TouchScreenKeyboard keyboard;
    private string inputID = "";
    private string inputPW = "";

    // 테스트용 로그인 자격 증명
    private string user = "NoonSong";
    private string password = "1906";

    private void Start()
    {
        // 입력 필드에 이벤트 리스너 추가
        inputField_ID.onEndEdit.AddListener(OnIDFieldEndEdit);
        inputField_PW.onEndEdit.AddListener(OnPasswordFieldEndEdit);
    }

    void Update()
    {
        // 터치스크린 키보드 상태 확인 및 입력 처리
        if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            // 입력된 아이디와 비밀번호를 변수에 저장
            inputID = inputField_ID.text;
            inputPW = inputField_PW.text;

            // 입력 확인 및 로그인 처리
            if (inputID == user && inputPW == password)
            {
                Debug.Log("로그인 성공!");
                SceneManager.LoadScene("Merge-TutorialScene"); // 씬 이동
            }
            else
            {
                Debug.Log("로그인 실패");
            }

            // 키보드 객체를 null로 설정하여 반복 체크 방지
            keyboard = null;
        }
    }

    /// <summary>
    /// 아이디 입력 필드의 입력이 완료되었을 때 호출되는 메서드
    /// </summary>
    private void OnIDFieldEndEdit(string input)
    {
        // 터치스크린 키보드를 열어 비밀번호 입력을 대기
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    /// <summary>
    /// 비밀번호 입력 필드의 입력이 완료되었을 때 호출되는 메서드
    /// </summary>
    private void OnPasswordFieldEndEdit(string input)
    {
        // 터치스크린 키보드를 열어 비밀번호 입력을 대기
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

        // 비밀번호 입력 완료 시 로그인 시도
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // 현재 키보드 상태를 업데이트하고 로그인 시도
            Update();
        }
    }
}