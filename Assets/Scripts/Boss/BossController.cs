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
    private void Start()
    {
        Application.targetFrameRate = 60;
        stat = GetComponent<Stat>();
        animator = GetComponent<Animator>();
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
        BaseState MoveState = new BossMoveState(this, rigid2D, animator, stat);
        BaseState PatternState = new Boss1PatternState(this, rigid2D, animator, stat);

        //상태 추가
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
    }

    public override void OnHit(float damage)
    {

    }
}
