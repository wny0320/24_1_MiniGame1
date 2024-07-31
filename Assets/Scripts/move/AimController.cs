using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimController : MonoBehaviour
{
    PlayerController playerController;
    Bullet bullet;
    Vector3 inputPos;

    [SerializeField]
    private Transform aimObject;

    public Transform AimObject => aimObject; // aimObject의 위치를 공개하는 프로퍼티


    // 이렇게 쓰면 유니티 내부에서 데이터를 편집 할 수 있으며, Range는 해당 범위 내에서 실린더를 통해 조절 가능함
    [SerializeField, Range(0, 1)]
    private float aimSpeed;
    private bool cursorLockFlag = false;
    private bool cursorVisibleFlag = false;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        bullet = GetComponent<Bullet>();    
    }

    private void mouseCursorTracking()
    {
        // 마우스 커서 위치를 따라 이동하도록 
        //  ScreenToWorld 는 게임창에서 마우스 위치를 인식하게 해준다
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
       
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// 마우스가 창 밖으로 나가지 못하게 하는 함수, 왼쪽 Ctrl키로 on/off
    /// </summary>
    private void mouseCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            cursorLockFlag = !cursorLockFlag;
        if (cursorLockFlag == true)
            Cursor.lockState = CursorLockMode.Confined;
        else if (cursorLockFlag == false)
            Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// 마우스 커서를 숨기는 함수, 왼쪽 Alt키로 on/off
    /// </summary>
    private void mouseCursorVisible()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            cursorVisibleFlag = !cursorVisibleFlag;
        if (cursorVisibleFlag == true)
            Cursor.visible = true;
        else if (cursorLockFlag == false)
            Cursor.visible = false;
    }


   
    void Update()
    {
        //최대한 업데이트 내부에서 코드짜지 말고 함수로 외부로 뺄 것
        mouseCursorTracking();
        mouseCursorLock();
        mouseCursorVisible();
      

    }
    

    

   
}
