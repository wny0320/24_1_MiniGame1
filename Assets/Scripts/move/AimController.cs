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
        // 마우스 커서 위치를 따라 이동하도록 
        //  ScreenToWorld 는 게임창에서 마우스 위치를 인식하게 해준다
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePos.z = 0;

        float AimSpeed = 0.8f; //  에임이 마우스 커서를 따라오는 속도
        transform.position = Vector3.Lerp(transform.position, mousePos, AimSpeed);

;
    }
}
