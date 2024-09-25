using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour
{
    public Image targetImage; // 변경할 이미지
    public Sprite sprite1; // 기본 스프라이트
    public Sprite sprite2; // 클릭 시 바뀔 스프라이트
    public float switchBackDelay = 1f; // 되돌아오는 시간

    void Start()
    {
        targetImage.sprite = sprite1; // 기본 스프라이트 설정
    }

    public void OnButtonClick()
    {
        targetImage.sprite = sprite2; // 스프라이트 변경
        StartCoroutine(SwitchBack());
    }

    private IEnumerator SwitchBack()
    {
        yield return new WaitForSeconds(switchBackDelay); // 지정한 시간 대기
        targetImage.sprite = sprite1; // 원래 스프라이트로 되돌리기
    }
}
