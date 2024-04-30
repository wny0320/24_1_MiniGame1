using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class BossPatternState : BaseState
{
    Transform playerTrans;
    // 플레이어와의 거리
    float playerDis;
    // 플레이어 방향 단위 벡터
    Vector3 playerDir;
    // 임시 단위미터
    float unitDistance = 1f;
    // 패턴의 갯수
    int patternCount = 6;
    // 패턴 함수가 실행됐는지 파악할 flag
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
        // 패턴마다 조건이 충족되었는지 담고 있는 트리거 어레이
        bool[] patternTrigger = new bool[patternCount];
        #region 패턴 조건 조건문
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
        //체력 관련 조건 아직
        #endregion
        #region 랜덤 패턴 Invoke
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
    #region 패턴 함수들
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
