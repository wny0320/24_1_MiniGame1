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
        //���� ����
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator);

        //���� �߰�
        states.Add(BossState.Move, MoveState);

        //state machine �ʱⰪ
        stateMachine = new StateMachine(MoveState);
    }
}
