using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField, Range(1, 100)]
   // private float bulletSpeed;
    private Collider2D col;
    public GameObject Bullet;


  
    



    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        InitStateMachine();

        Manager.Game.Player = gameObject;
    }

    void Update()
    {
        stateMachine?.StateUpdateFunc();
       
        //Shoot();

    }
    
   /* void Shoot()
    {
        Vector3 inputPos;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z; // 카메라와 마우스의 거리를 고려하여 z 좌표 설정
            inputPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            GameObject nowBullet = Manager.Bullet.GetBullet();
            inputPos = Vector3.Normalize(inputPos - transform.position); // 상대적 위치를 정규화
            Manager.Bullet.BulletInit(nowBullet,transform.position,inputPos);
        }
        
    }*/

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //상태 생성
        BaseState IdleState = new PlayerIdleState(this, rigid2D, animator);
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator, transform);
        BaseState DodgeState = new PlayerDodgeState(this, rigid2D, animator, col);
        BaseState ParryState = new PlayerParryState(this, rigid2D, animator);

        //상태 추가
        states.Add(PlayerState.Idle, IdleState);
        states.Add(PlayerState.Move, MoveState);
        states.Add(PlayerState.Dodge, DodgeState);
        states.Add(PlayerState.Parrying, ParryState);

        //state machine 초기값- Idle
        stateMachine = new StateMachine(IdleState);
    }

    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(PlayerState)s]);
    }

   


    public override void OnHit(float damage)
    {
    }
}
