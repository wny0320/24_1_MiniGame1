using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Play_ui로 사용할 GameObject를 연결합니다.
    public GameObject playUI;

    // Start 버튼을 연결합니다.
    public Button startButton;

    void Start()
    {
        // 처음에 Play_ui를 비활성화 상태로 설정합니다.
        playUI.SetActive(false);

        // Start 버튼에 onClick 이벤트를 연결합니다.
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    // Start 버튼이 눌렸을 때 호출되는 함수
    void OnStartButtonClick()
    {
        // Play_ui를 활성화합니다.
        playUI.SetActive(true);
    }
}
