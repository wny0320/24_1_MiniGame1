using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public CustomSceneManager customSceneManager; // Inspector���� �Ҵ�
        
    public void PlayGame()
    {
        // �ε� ������ ��ȯ
        customSceneManager.ChangeScene(SceneType.Stage1); // SceneType�� enum �Ǵ� ���ǵ� Ÿ��
    }
}
