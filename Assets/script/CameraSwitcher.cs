using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1; // �÷��̾ ���ߴ� ī�޶�
    public Camera camera2; // Start ȭ���� ���ߴ� ī�޶�
    public GameObject startCanvas; // Start ��ư�� �ִ� Canvas
    public GameObject[] otherObjects; // �Ѿ� �� ������ ������Ʈ��

    // Start ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnStartButtonPressed()
    {
        // Camera 2 (Start ȭ��) ��Ȱ��ȭ
        camera2.gameObject.SetActive(false);

        // Camera 1 (�÷��̾�) Ȱ��ȭ
        camera1.gameObject.SetActive(true);

        // Start Canvas ��Ȱ��ȭ
        startCanvas.SetActive(false);

        // ������ ������Ʈ Ȱ��ȭ
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(true);
        }
    }

    void Start()
    {
        // ������ �� Camera 1�� ���� �ְ� Camera 2�� ���� ���·� ����
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(true);
    }
}
