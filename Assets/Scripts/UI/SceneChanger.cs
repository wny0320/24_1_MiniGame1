using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위한 네임스페이스

public class SceneChanger : MonoBehaviour
{
    public Button changeSceneButton; // 버튼 컴포넌트를 연결할 변수
    public SceneType sceneToLoad; // 전환할 씬을 지정하는 변수

    void Start()
    {
        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(OnChangeSceneButtonClicked); // 버튼 클릭 이벤트에 메서드 연결
        }
        else
        {
            Debug.LogError("Change Scene Button is not assigned.");
        }
    }

    void OnChangeSceneButtonClicked()
    {
        // CustomSceneManager 인스턴스가 존재하는지 확인
        if (CustomSceneManager.Instance != null)
        {
            CustomSceneManager.Instance.ChangeScene(sceneToLoad); // 씬을 전환
        }
        else
        {
            Debug.LogError("CustomSceneManager instance is missing.");
        }
    }
}
