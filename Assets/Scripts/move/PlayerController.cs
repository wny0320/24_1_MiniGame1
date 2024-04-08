using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    [SerializeField, Range(0,10)] 
    float walkSpeed = 6.0f;
    [SerializeField]
    private GameObject ground;

    // 애니메이션
    //Animator animator;  


    //상태 패턴
    #region StateMachine
    protected StateMachine stateMachine;
    protected Dictionary<PlayerState, BaseState> states = new Dictionary<PlayerState, BaseState>();
    #endregion


    void Start()
    {
        InitStateMachine();
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();

        // this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        stateMachine?.StateUpdateFunc();
        PlayerMove();
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
        states.Add(PlayerState.Move, MoveState);

        //state machine 초기값
        stateMachine = new StateMachine(MoveState);
    }


    /// <summary>
    /// 플레이어가 움직이는 맵이 네모라는 가정, 다른 모양일 경우 수정 필요
    /// 추가로 플레이어의 크기를 고려함
    /// </summary>
    /// <returns>Vector3, player의 clipping된 좌표</returns>
    private Vector3 PlayerClipping()
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

    private void PlayerMove()
    {
        // 좌우 앞뒤 이동

        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;


        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // 키보드에 따른 방향으로 이동속도 만큼 이동
        transform.position = PlayerClipping();
    }
}
