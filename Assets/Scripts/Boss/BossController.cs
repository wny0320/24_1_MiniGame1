using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    public Transform playerTrans;
    /// <summary>
    /// 패턴이 시작되는 시간
    /// </summary>
    public float patternTime = 5f;
    /// <summary>
    /// 스톱워치와 같은 타이머, 초기값 0
    /// </summary>
    public float patternTimer = 0f;
    /// <summary>
    /// 단위 거리
    /// </summary>
    public float unitDis = 0.1f;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        stat = GetComponent<Stat>();
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
        BaseState PatternState = new BossPatternState(this, rigid2D, animator, stat);
        BaseState Pattern1 = new Boss1Pattern1(this, rigid2D, animator, stat);

        //상태 추가
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);
        states.Add(Boss1Pattern.Pattern1, Pattern1);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);

        //stateMachine.SetState(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
    }
    public void GetPlayerTrans()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;
        else
            return;
    }
}
