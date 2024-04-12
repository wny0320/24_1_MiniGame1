using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    const string PLAYER_MOVE = "isMoving";
    const string X_DIR = "xDir";
    const string Y_DIR = "yDir";

    Transform transform;

    float walkSpeed = 6.0f;//임시

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

        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // 키보드에 따른 방향으로 이동속도 만큼 이동
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
        /// 플레이어가 움직이는 맵이 네모라는 가정, 다른 모양일 경우 수정 필요
        /// 추가로 플레이어의 크기를 고려함
        /// </summary>
        /// <returns>Vector3, player의 clipping된 좌표</returns>

        if (controller.ground == null) return Vector3.zero;

        Transform groundTrans = controller.ground.transform;
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
}
