using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        InitStateMachine();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
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
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator);

        //상태 추가
        states.Add(BossState.Move, MoveState);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);
    }
}
