using UnityEngine;

public class CustomSceneManager : MonoBehaviour
{
    private static CustomSceneManager instance; // 싱글톤 인스턴스

    // CustomSceneManager 인스턴스에 접근하기 위한 정적 프로퍼티
    public static CustomSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CustomSceneManager instance not found!");
            }
            return instance;
        }
    }

    // 싱글톤 패턴 설정
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되더라도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스는 삭제
        }
    }

    // 씬 변경 메서드
    public void ChangeScene(SceneType scene)
    {
        // SceneManager를 통해 씬 전환
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
    }
}
