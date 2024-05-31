using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    //해당 속성들이 보스 공통일지는 모름
    public NowBoss nowBoss;
    public float noDamagedTime = 0f;
    public bool phase2Flag = false;
    public float timeAdd = 0f;
    public bool timeAddFlag = false;
    // 2페이즈 패턴 시간 배율
    public float phase2TimeMul = 1.2f;
    // 2페이즈 데미지 배율
    public float phase2DamageMul = 1.3f;
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
        //상태 생성
        BaseState MoveState = new Boss1MoveState(this, rigid2D, animator, stat);
        
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
        noDamagedTime = 0f;
    }
    public void PatternTimeAdder(float _time)
    {
        timeAdd = _time;
        timeAddFlag = true;
    }
}
