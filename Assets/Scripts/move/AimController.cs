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

    public Transform AimObject => aimObject; // aimObject�� ��ġ�� �����ϴ� ������Ƽ


    // �̷��� ���� ����Ƽ ���ο��� �����͸� ���� �� �� ������, Range�� �ش� ���� ������ �Ǹ����� ���� ���� ������
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
        // ���콺 Ŀ�� ��ġ�� ���� �̵��ϵ��� 
        //  ScreenToWorld �� ����â���� ���콺 ��ġ�� �ν��ϰ� ���ش�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
       
        transform.position = Vector3.Lerp(transform.position, mousePos, aimSpeed);
    }
    /// <summary>
    /// ���콺�� â ������ ������ ���ϰ� �ϴ� �Լ�, ���� CtrlŰ�� on/off
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


   
    void Update()
    {
        //�ִ��� ������Ʈ ���ο��� �ڵ�¥�� ���� �Լ��� �ܺη� �� ��
        mouseCursorTracking();
        mouseCursorLock();
        mouseCursorVisible();
      

    }
    

    

   
}
