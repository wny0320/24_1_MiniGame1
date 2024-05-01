using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
<<<<<<< Updated upstream
    // �̷��� ���� ����Ƽ ���ο��� �����͸� ���� �� �� ������, Range�� �ش� ���� ������ �Ǹ����� ���� ���� ������
    [SerializeField, Range(0,1)]
    private float aimSpeed;
    private bool cursorLockFlag = false;
    private bool cursorVisibleFlag = false;
=======
    // ????? ???? ????? ???��??? ??????? ???? ?? ?? ??????, Range?? ??? ???? ?????? ??????? ???? ???? ??????
    [SerializeField, Range(0, 1)]
    private float aimSpeed;
    private bool cursorLockFlag = false;
    private bool cursorVisibleFlag = false;

    private void mouseCursorTracking()
    {
        // ???�J ��?? ????? ???? ???????? 
        //  ScreenToWorld ?? ????????? ???�J ????? ?��???? ?????
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
        // ??? ???��????? ?? ?????? ????? ?????? ??? ??��??? ????
        //float aimSpeed = 0.8f; //  ?????? ???�J ��???? ??????? ???
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// ???�J?? ? ?????? ?????? ????? ??? ???, ???? Ctrl??? on/off
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
    /// ???�J ��???? ????? ???, ???? Alt??? on/off
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
        // ���콺 Ŀ�� ��ġ�� ���� �̵��ϵ��� 
        //  ScreenToWorld �� ����â���� ���콺 ��ġ�� �ν��ϰ� ���ش�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
        // �Լ� ���ο����� �� ������ �ƴ϶�� ������ �Լ� �ܺο��� ����
        //float aimSpeed = 0.8f; //  ������ ���콺 Ŀ���� ������� �ӵ�
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// ���콺�� â ������ ������ ���ϰ� �ϴ� �Լ�, ���� CtrlŰ�� on/off
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
    /// ���콺 Ŀ���� ����� �Լ�, ���� AltŰ�� on/off
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
    // �ʿ���� �̺�Ʈ �Լ���� �� ���ٰ�
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //�ִ��� ������Ʈ ���ο��� �ڵ�¥�� ���� �Լ��� �ܺη� �� ��
=======
        //????? ??????? ???��??? ?????? ???? ????? ??��? ?? ??
>>>>>>> Stashed changes
        mouseCursorTracking();
        mouseCursorLock();
        mouseCursorVisible();
    }
}



