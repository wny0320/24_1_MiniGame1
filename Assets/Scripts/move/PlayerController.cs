using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
<<<<<<< Updated upstream
    private Collider2D col;
=======
    Rigidbody2D rigid2D;
    // 코딩 스타일이고 회사마다 다르긴 한데 보통 private인 변수는 첫글자 소문자, public은 첫글자 대문자를 주로 씁니다
    [SerializeField, Range(0, 10)] // 마우스와 마찬가지
    float walkSpeed = 6.0f;              // 걷는 속도
    [SerializeField]
    private GameObject ground;

    // 애니메이션
    //Animator animator;  
>>>>>>> Stashed changes

    // 여기는 스타일마다 달라서 함수라면 모두 대문자 쓰시는 분들이 있고,
    // 변수와 마찬가지로 private라면 소문자 public이라면 대문자 쓰시는 분들 있어서 이건 취향입니다
    private void playerMove()
    {
        // 좌우 앞뒤 이동

<<<<<<< Updated upstream
=======
        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;



        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // 키보드에 따른 방향으로 이동속도 만큼 이동
        //transform.position = playerClipping();
    }
    /// <summary>
    /// 플레이어가 움직이는 맵이 네모라는 가정, 다른 모양일 경우 수정 필요
    /// 추가로 플레이어의 크기를 고려함
    /// </summary>
    /// <returns>Vector3, player의 clipping된 좌표</returns>
    private Vector3 playerClipping()
    {
        if (ground == null) return Vector3.zero;

        Transform groundTrans = ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        Vector3 playerPos = transform.position;
        Vector3 playerScale = transform.lossyScale;

        // 클립핑할 사각형의 왼쪽 아래점과 오른쪽 위의 점을 구함
        Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + playerScale / 2;
        Vector2 clipSquareRightUp = groundPos + groundScale / 2 - playerScale / 2;

        float clipedXPos = Mathf.Clamp(playerPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(playerPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
    }
    // Start is called before the first frame update
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        stateMachine?.StateUpdateFunc();
    }

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
=======
        //마찬가지로 함수로 만들어서 업데이트 함수 안에 넣어주세요
        playerMove();
>>>>>>> Stashed changes
    }
}