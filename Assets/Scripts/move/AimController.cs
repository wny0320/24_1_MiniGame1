using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 Ŀ�� ��ġ�� ���� �̵��ϵ��� 
        //  ScreenToWorld �� ����â���� ���콺 ��ġ�� �ν��ϰ� ���ش�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePos.z = 0;

        float AimSpeed = 0.8f; //  ������ ���콺 Ŀ���� ������� �ӵ�
        transform.position = Vector3.Lerp(transform.position, mousePos, AimSpeed);

;
    }
}
