using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    const string PLAYER_MOVE = "isMoving";
    const string X_DIR = "xDir";
    const string Y_DIR = "yDir";

    Transform transform;

    float walkSpeed = 6.0f;//�ӽ�

    public PlayerMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Transform transform = null)
        : base(controller, rb, animator)
    {
        this.transform = transform;

        Manager.Input.PlayerMove -= PlayerMove;
        Manager.Input.PlayerMove += PlayerMove;
    }

    public override void OnStateEnter()
    {
        animator.SetBool(PLAYER_MOVE, true);
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
        animator.SetBool(PLAYER_MOVE, false);
    }


    private void PlayerMove()
    {
        if (!Manager.Game.isAlive) return;

        Manager.Input.isPlayerMove = true;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        animator.SetFloat(X_DIR, inputX);
        animator.SetFloat(Y_DIR, inputY);

        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // Ű���忡 ���� �������� �̵��ӵ� ��ŭ �̵�
        transform.position = PlayerClipping();

        if (inputX == 0 && inputY == 0)
        {
            Manager.Input.isPlayerMove = false;
            controller.ChangeState(PlayerState.Idle);
        }
    }

    private Vector3 PlayerClipping()
    {
        /// <summary>
        /// �÷��̾ �����̴� ���� �׸��� ����, �ٸ� ����� ��� ���� �ʿ�
        /// �߰��� �÷��̾��� ũ�⸦ �����
        /// </summary>
        /// <returns>Vector3, player�� clipping�� ��ǥ</returns>

        if (controller.ground == null) return Vector3.zero;

        Transform groundTrans = controller.ground.transform;
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
}
