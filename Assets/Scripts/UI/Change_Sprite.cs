using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour
{
    public Image targetImage; // ������ �̹���
    public Sprite sprite1; // �⺻ ��������Ʈ
    public Sprite sprite2; // Ŭ�� �� �ٲ� ��������Ʈ
    public float switchBackDelay = 1f; // �ǵ��ƿ��� �ð�

    void Start()
    {
        targetImage.sprite = sprite1; // �⺻ ��������Ʈ ����
    }

    public void OnButtonClick()
    {
        targetImage.sprite = sprite2; // ��������Ʈ ����
        StartCoroutine(SwitchBack());
    }

    private IEnumerator SwitchBack()
    {
        yield return new WaitForSeconds(switchBackDelay); // ������ �ð� ���
        targetImage.sprite = sprite1; // ���� ��������Ʈ�� �ǵ�����
    }
}
