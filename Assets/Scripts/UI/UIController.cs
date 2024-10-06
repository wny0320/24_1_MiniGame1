using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Play_ui�� ����� GameObject�� �����մϴ�.
    public GameObject playUI;

    // Start ��ư�� �����մϴ�.
    public Button startButton;

    void Start()
    {
        // ó���� Play_ui�� ��Ȱ��ȭ ���·� �����մϴ�.
        playUI.SetActive(false);

        // Start ��ư�� onClick �̺�Ʈ�� �����մϴ�.
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    // Start ��ư�� ������ �� ȣ��Ǵ� �Լ�
    void OnStartButtonClick()
    {
        // Play_ui�� Ȱ��ȭ�մϴ�.
        playUI.SetActive(true);
    }
}
