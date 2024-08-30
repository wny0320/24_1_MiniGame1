using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� ���ӽ����̽�

public class SceneChanger : MonoBehaviour
{
    public Button changeSceneButton; // ��ư ������Ʈ�� ������ ����
    public SceneType sceneToLoad; // ��ȯ�� ���� �����ϴ� ����

    void Start()
    {
        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(OnChangeSceneButtonClicked); // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        }
        else
        {
            Debug.LogError("Change Scene Button is not assigned.");
        }
    }

    void OnChangeSceneButtonClicked()
    {
        // CustomSceneManager �ν��Ͻ��� �����ϴ��� Ȯ��
        if (CustomSceneManager.Instance != null)
        {
            CustomSceneManager.Instance.ChangeScene(sceneToLoad); // ���� ��ȯ
        }
        else
        {
            Debug.LogError("CustomSceneManager instance is missing.");
        }
    }
}
