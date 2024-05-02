using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossPatternState : BaseState
{
    // 패턴의 갯수
    int patternCount = 6;
    BossController bossController;
    public BossPatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        GetBossController();
        PatternSelect();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    private void PatternSelect()
    {
        if (bossController == null)
            return;
        if (bossController.playerTrans == null)
            return;
        float playerDis = Vector3.Magnitude((Vector2)(bossController.playerTrans.position) - rb.position);
        // 패턴마다 조건이 충족되었는지 담고 있는 트리거 어레이
        bool[] patternTrigger = new bool[patternCount];
        #region 패턴 조건 조건문
        if(playerDis > bossController.unitDis * 15f)
            patternTrigger[0] = true;
        if(playerDis < bossController.unitDis * 1.3f)
            patternTrigger[1] = true;
        if(playerDis < bossController.unitDis * 8f)
            patternTrigger[2] = true;
        if (playerDis > bossController.unitDis * 12f)
            patternTrigger[3] = true;
        if (playerDis < bossController.unitDis * 20f)
            patternTrigger[4] = true;
        //체력 관련 조건 아직
        #endregion
        #region 랜덤 패턴 State Change
        List<int> activeTriggerList = new List<int>();
        for(int i = 0; i < patternCount; i++)
        {
            if (patternTrigger[i] == true)
                activeTriggerList.Add(i);
        }
        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
        int selectedIndex = activeTriggerList[n];
        string patternName = "Pattern" + selectedIndex;
        Boss1Pattern targetPattern = (Boss1Pattern)Enum.Parse(typeof(Boss1Pattern), patternName);
        //controller.ChangeState(targetPattern);
        controller.ChangeState(BossState.Move);
        #endregion
    }
    public void GetBossController()
    {
        if (bossController == null)
        {
            if (controller == null)
                return;
            else
                bossController = controller as BossController;
        }
        return;
    }
    //#region 패턴 함수들
    //public async void Pattern0()
    //{
    //    Debug.Log("Pattern0 Invoked");
    //    float timer = 0f;
    //    float time = 2f;

    //    await new Task(() =>
    //    {
    //        while(timer < time)
    //        {
    //            timer += Time.deltaTime;
    //            rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 2.5f;
    //        }
    //    });
    //    //다음 패턴을 하게 값들을 초기화
    //    InitPlayerData();
    //}
    //public async void Pattern1()
    //{
    //    Debug.Log("Pattern1 Invoked");
    //    float time = 0.8f;
    //    int number = 5;
    //    await new Task (() =>
    //    {
    //        new WaitForSeconds(1f);
    //    });
    //    await new Task(() =>
    //    {
    //        for(int i = 0; i < number; i++)
    //        {
    //            new WaitForSeconds(time);
    //            //대검 찍기
    //        }
    //    });
    //    //스테이트 바꾸기
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern2()
    //{
    //    Debug.Log("Pattern2 Invoked");
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(1.3f);
    //    });
    //    float timer = 0f;
    //    float time = 1f;
    //    //대검 찌르기
    //    await new Task(() =>
    //    {
    //        while (timer < time)
    //        {
    //            timer += Time.deltaTime;
    //            rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 4f;
    //        }
    //    });
    //    //후딜레이
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(3f);
    //    });
    //    스테이트 바꾸기
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern3()
    //{
    //    Debug.Log("Pattern3 Invoked");
    //    float timer = 0f;
    //    float time = 4f;
    //    // 병사 소환
    //    await new Task(() =>
    //    {
    //        Vector3 realPlayerPos = playerTrans.position;
    //        new WaitForSeconds(0.8f);
    //        while(timer < time)
    //        {
    //            //병사 조준
    //        }
    //        //화살 발사
    //    });
    //    //스테이트 바꾸기
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern4()
    //{
    //    Debug.Log("Pattern4 Invoked");
    //    await new Task(() =>
    //    {
    //        //땅에 칼 찍기
    //        new WaitForSeconds(1.5f);
    //        float timer = 0f;
    //        float time = 1f;
    //        //점프
    //        while(timer < time)
    //        {
    //            rb.transform.position += Vector3.up * Time.deltaTime;
    //        }
    //        //내려찍기
    //        while (timer < time)
    //        {
    //            rb.transform.position += Vector3.down * Time.deltaTime;
    //        }
    //    });
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern5()
    //{
    //    Debug.Log("Pattern5 Invoked");
    //    await new Task(() =>
    //    {
    //        // 100회복할때까지 회복
    //        // 30% 뎀감
    //    });
    //}
    //#endregion
}
