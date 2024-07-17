using UnityEngine;

public class RotateOnTab : MonoBehaviour
{
    public float rotationSpeed = -240.0f; // 초당 회전 속도(-240도)
    private bool isRotating = false; // 현재 회전 중인지 여부를 추적
    private float targetRotation = 90.0f; // 목표 회전 각도(90도)
    private float currentRotation = 0.0f; // 현재 회전된 각도 추적

    void Update()
    {
        // Tab 키가 눌리고 현재 회전 중이 아닐 때
        if (Input.GetKeyDown(KeyCode.Tab) && !isRotating)
        {
            isRotating = true; // 회전 시작
            currentRotation = 0.0f; // 현재 회전 각도 초기화
        }

        // 회전 중일 때
        if (isRotating)
        {
            float rotationStep = rotationSpeed * Time.deltaTime; // 이번 프레임에서의 회전량 계산
            transform.Rotate(0, 0, rotationStep); // 회전 적용
            currentRotation += Mathf.Abs(rotationStep); // 현재 회전 각도 업데이트

            // 현재 회전 각도가 목표 회전 각도에 도달(또는 초과)했는지 확인
            if (currentRotation >= targetRotation)
            {
                // 회전 초과량 보정
                float overshoot = currentRotation - targetRotation;
                transform.Rotate(0, 0, rotationSpeed > 0 ? -overshoot : overshoot);
                isRotating = false; // 회전 중 상태 해제
            }
        }
    }
}
