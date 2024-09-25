using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public CustomSceneManager customSceneManager; // Inspector에서 할당
        
    public void PlayGame()
    {
        // 로딩 씬으로 전환
        customSceneManager.ChangeScene(SceneType.Stage1); // SceneType은 enum 또는 정의된 타입
    }
}
