using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    // 현재 패턴 코루틴
    private Coroutine patternCoroutine;
    // 외부에서 넣어야하는 값
    public NowBoss bossNum = NowBoss.Null;
    // 보스 패턴의 갯수
    private int bossPatternCount;
    // 플레이어의 Transform
    public Transform playerTrans;
    // 패턴이 시작되는 시간
    public float patternTime = 5f;
    // 스톱워치와 같은 타이머, 초기값 0
    public float patternTimer = 0f;
    // 단위 거리
    public float unitDis = 0.1f;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        stat = GetComponent<Stat>();
        animator = GetComponent<Animator>();
        InitStateMachine();
    }

    void Update()
    {
        stateMachine?.StateUpdateFunc();
        GetPlayerTrans();
    }

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //상태 생성
        BaseState MoveState = new BossMoveState(this, rigid2D, animator, stat);
        //BaseState PatternState = new BossPatternState(this, rigid2D, animator, stat);

        //상태 추가
        states.Add(BossState.Move, MoveState);
        //states.Add(BossState.Pattern, PatternState);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
        if((BossState)s == BossState.Pattern)
        {

        }
    }
    public void GetPlayerTrans()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;
        else
            return;
    }

    public override void OnHit(float damage)
    {

    }
    //private void PatternSelect()
    //{
    //    // 플레이어의 위치를 받아오지 못했다면 return
    //    if (playerTrans == null)
    //        return;
    //    // 보스가 정해지지 않았다면 return
    //    if (bossNum == BossNum.Null)
    //        return;
    //    float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rigid2D.position);
    //    // 패턴마다 조건이 충족되었는지 담고 있는 트리거 어레이
    //    #region 패턴 조건 조건문
    //    bool[] patternTrigger = new bool[bossPatternCount];
    //    List<int> activeTriggerList = new List<int>();
    //    if (bossNum == BossNum.Boss1)
    //    {
    //        bossPatternCount = Enum.GetValues(typeof(Boss1Pattern)).Length;
    //        patternTrigger = new bool[bossPatternCount];

    //        //보스 1 패턴 조건
    //        patternTrigger[0] = false;
    //        if (playerDis < unitDis * 1.3f)
    //            patternTrigger[1] = true;
    //        if (playerDis < unitDis * 8f)
    //            patternTrigger[2] = true;
    //        if (playerDis > unitDis * 12f)
    //            patternTrigger[3] = true;
    //        if (playerDis < unitDis * 20f)
    //            patternTrigger[4] = true;

    //        for (int i = 0; i < bossPatternCount; i++)
    //        {
    //            if (patternTrigger[i] == true)
    //                activeTriggerList.Add(i);
    //        }

    //        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
    //        int selectedIndex = activeTriggerList[n];
    //        string patternName = "Pattern" + selectedIndex;
    //        Boss1Pattern targetPattern = (Boss1Pattern)Enum.Parse(typeof(Boss1Pattern), patternName);
    //        //현재 구현되지 않았으므로 Move상태로 설정중
    //        //controller.ChangeState(targetPattern);
    //        ChangeState(BossState.Move);
    //    }
    //    if (bossNum == BossNum.Boss2)
    //    {
    //        //bossPatternCount = Enum.GetValues(typeof(Boss2Pattern)).Length;
    //        //patternTrigger = new bool[bossPatternCount];
    //    }
    //    #endregion
    //}
    private IEnumerator Boss1Pattern1()
    {
        yield return null;
    }
}
