using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    [SerializeField, Range(0,10)] 
    float walkSpeed = 6.0f;
    [SerializeField]
    private GameObject ground;

    // �ִϸ��̼�
    //Animator animator;  


    //���� ����
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
        //���� ����
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator);

        //���� �߰�
        states.Add(PlayerState.Move, MoveState);

        //state machine �ʱⰪ
        stateMachine = new StateMachine(MoveState);
    }


    /// <summary>
    /// �÷��̾ �����̴� ���� �׸��� ����, �ٸ� ����� ��� ���� �ʿ�
    /// �߰��� �÷��̾��� ũ�⸦ �����
    /// </summary>
    /// <returns>Vector3, player�� clipping�� ��ǥ</returns>
    private Vector3 PlayerClipping()
    {
        if (ground == null) return Vector3.zero;

        Transform groundTrans = ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        Vector3 playerPos = transform.position;
        Vector3 playerScale = transform.lossyScale;

        // Ŭ������ �簢���� ���� �Ʒ����� ������ ���� ���� ����
        Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + playerScale / 2;
        Vector2 clipSquareRightUp = groundPos + groundScale / 2 - playerScale / 2;

        float clipedXPos = Mathf.Clamp(playerPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(playerPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
    }

    private void PlayerMove()
    {
        // �¿� �յ� �̵�

        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;


        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // Ű���忡 ���� �������� �̵��ӵ� ��ŭ �̵�
        transform.position = PlayerClipping();
    }
}
