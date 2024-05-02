using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    public Transform playerTrans;
    public float moveSpeed = 1f;
    /// <summary>
    /// ������ ���۵Ǵ� �ð�
    /// </summary>
    public float patternTime = 5f;
    /// <summary>
    /// �����ġ�� ���� Ÿ�̸�, �ʱⰪ 0
    /// </summary>
    public float patternTimer = 0f;
    /// <summary>
    /// ���� �Ÿ�
    /// </summary>
    public float unitDis = 1f;
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
        GetPlayerTrans();
    }

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //���� ����
        BaseState MoveState = new BossMoveState(this, rigid2D, animator);
        BaseState PatternState = new BossPatternState(this, rigid2D, animator);

        //���� �߰�
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);
        //states.Add(Boss1Pattern.Pattern1, );

        //state machine �ʱⰪ
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
