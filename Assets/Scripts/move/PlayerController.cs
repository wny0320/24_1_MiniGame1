using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController, IReceiveAttack
{
    [SerializeField, Range(1, 100)]
   // private float bulletSpeed;
    private Collider2D col;
    public GameObject Bullet;



    public bool isDodging = false;
    public bool isParrying = false;
    public bool canParrying = true;

    private float parryingCoolTime = 3f;
    



    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        stat = GetComponent<Stat>();

        InitStateMachine();

        Manager.Game.Player = gameObject;


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
        //BaseState IdleState = new PlayerIdleState(this, rigid2D, animator);
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator, transform);
        BaseState DodgeState = new PlayerDodgeState(this, rigid2D, animator, col);
        BaseState ParryState = new PlayerParryState(this, rigid2D, animator);
        BaseState Diestate = new DieState(this, rigid2D, animator, transform);

        //상태 추가
        //states.Add(PlayerState.Idle, IdleState);
        states.Add(PlayerState.Move, MoveState);
        states.Add(PlayerState.Dodge, DodgeState);
        states.Add(PlayerState.Parrying, ParryState);
        states.Add(PlayerState.Die, Diestate);

        //state machine 초기값- Idle
        stateMachine = new StateMachine(MoveState);
    }

    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(PlayerState)s]);
    }

    public void Parrying()
    {
        if (!canParrying) return;
        canParrying = false;
        StartCoroutine(ParryingCoolTime());
        ChangeState(PlayerState.Parrying);
    }

    private IEnumerator ParryingCoolTime()
    {
        yield return new WaitForSeconds(parryingCoolTime);
        canParrying = true;
    }


    public void OnHit(float damage)
    {
        if(!isAlive) return;

        if(isParrying)
        {
            Debug.Log("player parryed");
            return;
        }

        if (isDodging)
        {
            Debug.Log("player dodged");
            return;
        }

        Debug.Log("player hitted");
        float finalDamage = Math.Max(0, damage - stat.Defense);
        stat.Hp -= finalDamage;
        if(stat.Hp <= 0)
        {
            isAlive = false;
            Manager.Game.isAlive = false;
            //캐릭터 사망 처리
            ChangeState(PlayerState.Die);
        }
    }


    
}
