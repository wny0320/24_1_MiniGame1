using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        InitStateMachine();
    }

    void Update()
    {
        stateMachine?.StateUpdateFunc();
    }

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //상태 생성
        BaseState MoveState = new BossMoveState(this, rigid2D, animator);
        BaseState PatternState = new BossPatternState(this, rigid2D, animator);

        //상태 추가
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);

        //stateMachine.SetState(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
    }
}
