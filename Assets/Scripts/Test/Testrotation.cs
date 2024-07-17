using UnityEngine;

public class RotateOnTab : MonoBehaviour
{
    public float rotationSpeed = -240.0f; // �ʴ� ȸ�� �ӵ�(-240��)
    private bool isRotating = false; // ���� ȸ�� ������ ���θ� ����
    private float targetRotation = 90.0f; // ��ǥ ȸ�� ����(90��)
    private float currentRotation = 0.0f; // ���� ȸ���� ���� ����

    void Update()
    {
        // Tab Ű�� ������ ���� ȸ�� ���� �ƴ� ��
        if (Input.GetKeyDown(KeyCode.Tab) && !isRotating)
        {
            isRotating = true; // ȸ�� ����
            currentRotation = 0.0f; // ���� ȸ�� ���� �ʱ�ȭ
        }

        // ȸ�� ���� ��
        if (isRotating)
        {
            float rotationStep = rotationSpeed * Time.deltaTime; // �̹� �����ӿ����� ȸ���� ���
            transform.Rotate(0, 0, rotationStep); // ȸ�� ����
            currentRotation += Mathf.Abs(rotationStep); // ���� ȸ�� ���� ������Ʈ

            // ���� ȸ�� ������ ��ǥ ȸ�� ������ ����(�Ǵ� �ʰ�)�ߴ��� Ȯ��
            if (currentRotation >= targetRotation)
            {
                // ȸ�� �ʰ��� ����
                float overshoot = currentRotation - targetRotation;
                transform.Rotate(0, 0, rotationSpeed > 0 ? -overshoot : overshoot);
                isRotating = false; // ȸ�� �� ���� ����
            }
        }
    }
}
