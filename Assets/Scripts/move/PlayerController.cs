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
    // �ڵ� ��Ÿ���̰� ȸ�縶�� �ٸ��� �ѵ� ���� private�� ������ ù���� �ҹ���, public�� ù���� �빮�ڸ� �ַ� ���ϴ�
    [SerializeField, Range(0, 10)] // ���콺�� ��������
    float walkSpeed = 6.0f;              // �ȴ� �ӵ�
    [SerializeField]
    private GameObject ground;

    // �ִϸ��̼�
    //Animator animator;  
>>>>>>> Stashed changes

    // ����� ��Ÿ�ϸ��� �޶� �Լ���� ��� �빮�� ���ô� �е��� �ְ�,
    // ������ ���������� private��� �ҹ��� public�̶�� �빮�� ���ô� �е� �־ �̰� �����Դϴ�
    private void playerMove()
    {
        // �¿� �յ� �̵�

<<<<<<< Updated upstream
=======
        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;



        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // Ű���忡 ���� �������� �̵��ӵ� ��ŭ �̵�
        //transform.position = playerClipping();
    }
    /// <summary>
    /// �÷��̾ �����̴� ���� �׸��� ����, �ٸ� ����� ��� ���� �ʿ�
    /// �߰��� �÷��̾��� ũ�⸦ �����
    /// </summary>
    /// <returns>Vector3, player�� clipping�� ��ǥ</returns>
    private Vector3 playerClipping()
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
        //���� ����
        BaseState IdleState = new PlayerIdleState(this, rigid2D, animator);
        BaseState MoveState = new PlayerMoveState(this, rigid2D, animator, transform);
        BaseState DodgeState = new PlayerDodgeState(this, rigid2D, animator, col);
        BaseState ParryState = new PlayerParryState(this, rigid2D, animator);

        //���� �߰�
        states.Add(PlayerState.Idle, IdleState);
        states.Add(PlayerState.Move, MoveState);
        states.Add(PlayerState.Dodge, DodgeState);
        states.Add(PlayerState.Parrying, ParryState);

        //state machine �ʱⰪ- Idle
        stateMachine = new StateMachine(IdleState);
    }

    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(PlayerState)s]);
=======
        //���������� �Լ��� ���� ������Ʈ �Լ� �ȿ� �־��ּ���
        playerMove();
>>>>>>> Stashed changes
    }
}