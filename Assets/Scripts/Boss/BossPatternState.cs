using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class BossPatternState : BaseState
{
    Transform playerTrans;
    // �÷��̾���� �Ÿ�
    float playerDis;
    // �÷��̾� ���� ���� ����
    Vector3 playerDir;
    // �ӽ� ��������
    float unitDistance = 1f;
    // ������ ����
    int patternCount = 6;
    // ���� �Լ��� ����ƴ��� �ľ��� flag
    bool funcFlag = false;
    public BossPatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        GetPlayerTrans();
        PatternSelect();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    private void GetPlayerTrans()
    {
        if (playerDir == null || playerTrans == null)
        {
            playerTrans = Manager.Game.Player.transform;
            playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
            playerDis = Vector3.Magnitude((Vector2)playerTrans.position - rb.position);
        }
    }
    private void PatternSelect()
    {
        if (playerDir == null || playerTrans == null)
            return;
        if (funcFlag == true)
            return;
        // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
        bool[] patternTrigger = new bool[patternCount];
        #region ���� ���� ���ǹ�
        if(playerDis > unitDistance * 15f)
            patternTrigger[0] = true;
        if(playerDis < unitDistance * 1.3f)
            patternTrigger[1] = true;
        if(playerDis < unitDistance * 8f)
            patternTrigger[2] = true;
        if (playerDis > unitDistance * 12f)
            patternTrigger[3] = true;
        if (playerDis < unitDistance * 20f)
            patternTrigger[4] = true;
        //ü�� ���� ���� ����
        #endregion
        #region ���� ���� Invoke
        List<int> activeTriggerList = new List<int>();
        for(int i = 0; i < patternCount; i++)
        {
            if (patternTrigger[i] == true)
                activeTriggerList.Add(i);
        }
        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
        int selectedIndex = activeTriggerList[n];
        string funcName = "Pattern" + selectedIndex;
        System.Reflection.MethodInfo patternMethod = GetType().GetMethod(funcName);
        funcFlag = true;
        patternMethod?.Invoke(this, null);
        #endregion
    }
    #region ���� �Լ���
    public void Pattern0()
    {
        Debug.Log("Pattern0 Invoked");
    }
    public void Pattern1()
    {
        Debug.Log("Pattern1 Invoked");
    }
    public void Pattern2()
    {
        Debug.Log("Pattern2 Invoked");
    }
    public void Pattern3()
    {
        Debug.Log("Pattern3 Invoked");
    }
    public void Pattern4()
    {
        Debug.Log("Pattern4 Invoked");
    }
    public void Pattern5()
    {
        Debug.Log("Pattern5 Invoked");
    }
    #endregion
}
