using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    //�ش� �Ӽ����� ���� ���������� ��
    public NowBoss nowBoss;
    public float noDamagedTime = 0f;
    public bool phase2Flag = false;
    public float timeAdd = 0f;
    public bool timeAddFlag = false;
    // 2������ ���� �ð� ����
    public float phase2TimeMul = 1.2f;
    // 2������ ������ ����
    public float phase2DamageMul = 1.3f;
    // ������ ���۵Ǵ� �ð�
    public float patternTime = 5f;
    // �����ġ�� ���� Ÿ�̸�, �ʱⰪ 0
    public float patternTimer = 0f;
    // ���� �Ÿ�
    public float unitDis = 0.1f;
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
        noDamagedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //���� ����
        BaseState MoveState = new Boss1MoveState(this, rigid2D, animator, stat);
        
        BaseState PatternState = new Boss1PatternState(this, rigid2D, animator, stat);

        //���� �߰�
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);

        //state machine �ʱⰪ
        stateMachine = new StateMachine(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
    }

    public override void OnHit(float damage)
    {
        noDamagedTime = 0f;
    }
    public void PatternTimeAdder(float _time)
    {
        timeAdd = _time;
        timeAddFlag = true;
    }
}
