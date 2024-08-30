using UnityEngine;

public class CustomSceneManager : MonoBehaviour
{
    private static CustomSceneManager instance; // �̱��� �ν��Ͻ�

    // CustomSceneManager �ν��Ͻ��� �����ϱ� ���� ���� ������Ƽ
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

    // �̱��� ���� ����
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǵ��� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ��� ����
        }
    }

    // �� ���� �޼���
    public void ChangeScene(SceneType scene)
    {
        // SceneManager�� ���� �� ��ȯ
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
    }
}
