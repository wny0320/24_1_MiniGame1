using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : BaseController, IReceiveAttack
{
    TestState testState;
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
        //StartCoroutine(testState.TestCo()); // ����� �۵���
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
        //���� ����
        testState = new TestState(this, rigid2D, animator, stat);
        //state machine �ʱⰪ
        stateMachine = new StateMachine(testState);
    }
    public override void ChangeState(Enum state)
    {
        // int s = Convert.ToInt32(state);
        // stateMachine.SetState(states[(BossState)s]);
    }

    public void OnHit(float damage)
    {

    }
}
