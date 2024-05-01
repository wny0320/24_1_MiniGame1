using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
<<<<<<< Updated upstream
    // 이렇게 쓰면 유니티 내부에서 데이터를 편집 할 수 있으며, Range는 해당 범위 내에서 실린더를 통해 조절 가능함
    [SerializeField, Range(0,1)]
    private float aimSpeed;
    private bool cursorLockFlag = false;
    private bool cursorVisibleFlag = false;
=======
    // ????? ???? ????? ???ο??? ??????? ???? ?? ?? ??????, Range?? ??? ???? ?????? ??????? ???? ???? ??????
    [SerializeField, Range(0, 1)]
    private float aimSpeed;
    private bool cursorLockFlag = false;
    private bool cursorVisibleFlag = false;

    private void mouseCursorTracking()
    {
        // ???콺 Ŀ?? ????? ???? ???????? 
        //  ScreenToWorld ?? ????????? ???콺 ????? ?ν???? ?????
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
        // ??? ???ο????? ?? ?????? ????? ?????? ??? ??ο??? ????
        //float aimSpeed = 0.8f; //  ?????? ???콺 Ŀ???? ??????? ???
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// ???콺?? ? ?????? ?????? ????? ??? ???, ???? Ctrl??? on/off
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
    /// ???콺 Ŀ???? ????? ???, ???? Alt??? on/off
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
    // ?????? ???? ?????? ?? ?????
    // Start is called before the first frame update
    //void Start()
    //{

    //}
>>>>>>> Stashed changes

    private void mouseCursorTracking()
    {
<<<<<<< Updated upstream
        // 마우스 커서 위치를 따라 이동하도록 
        //  ScreenToWorld 는 게임창에서 마우스 위치를 인식하게 해준다
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
        // 함수 내부에서만 쓸 변수가 아니라면 선언은 함수 외부에서 선언
        //float aimSpeed = 0.8f; //  에임이 마우스 커서를 따라오는 속도
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// 마우스가 창 밖으로 나가지 못하게 하는 함수, 왼쪽 Ctrl키로 on/off
    /// </summary>
    private void mouseCursorLock()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            cursorLockFlag = !cursorLockFlag;
        if(cursorLockFlag == true)
            Cursor.lockState = CursorLockMode.Confined;
        else if(cursorLockFlag == false)
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
    // 필요없는 이벤트 함수라면 꼭 없앨것
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //최대한 업데이트 내부에서 코드짜지 말고 함수로 외부로 뺄 것
=======
        //????? ??????? ???ο??? ?????? ???? ????? ??η? ?? ??
>>>>>>> Stashed changes
        mouseCursorTracking();
        mouseCursorLock();
        mouseCursorVisible();
    }
}



