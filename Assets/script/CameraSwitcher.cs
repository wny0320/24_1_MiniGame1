using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1; // 플레이어를 비추는 카메라
    public Camera camera2; // Start 화면을 비추는 카메라
    public GameObject startCanvas; // Start 버튼이 있는 Canvas
    public GameObject[] otherObjects; // 켜야 할 나머지 오브젝트들

    // Start 버튼이 눌렸을 때 호출되는 메서드
    public void OnStartButtonPressed()
    {
        // Camera 2 (Start 화면) 비활성화
        camera2.gameObject.SetActive(false);

        // Camera 1 (플레이어) 활성화
        camera1.gameObject.SetActive(true);

        // Start Canvas 비활성화
        startCanvas.SetActive(false);

        // 나머지 오브젝트 활성화
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(true);
        }
    }

    void Start()
    {
        // 시작할 때 Camera 1은 꺼져 있고 Camera 2만 켜진 상태로 설정
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(true);
    }
}
