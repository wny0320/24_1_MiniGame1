using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    const string PLAYER_MOVE = "isMoving";
    const string X_DIR = "xDir";
    const string Y_DIR = "yDir";

    Transform transform;
    PlayerController pc = null;

    Vector2 dir = Vector2.zero;
    float inputX = 0f;
    float inputY = 0f;
    float walkSpeed = 6.0f;//юс╫ц

    public PlayerMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Transform transform = null)
        : base(controller, rb, animator)
    {
        pc = controller as PlayerController;

        this.transform = transform;

        Manager.Input.PlayerMove -= PlayerMove;
        Manager.Input.PlayerMove += PlayerMove;
        Manager.Input.GetPlayerDir -= GetDir;
        Manager.Input.GetPlayerDir += GetDir;
    }

    public override void OnStateEnter()
    {
        animator.SetBool(PLAYER_MOVE, true);
    }


    public override void OnStateUpdate()
    {
        if (!Manager.Game.isAlive) return;

        animator.SetFloat(X_DIR, inputX);
        animator.SetFloat(Y_DIR, inputY);

        dir = new Vector2(inputX, inputY).normalized;
        transform.Translate(dir * Time.deltaTime * walkSpeed);

        if (Input.GetKeyDown(KeyCode.LeftShift)) Manager.Input.PlayerDodge.Invoke(dir);
        if (Input.GetKeyDown(KeyCode.Space)) pc.Parrying();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
        animator.SetBool(PLAYER_MOVE, false);
    }

    private Vector2 GetDir() => dir;

    private void PlayerMove()
    {
        if (pc.isDodging || pc.isParrying) return;

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX == 0 && inputY == 0)
        {
            dir = Vector2.zero;
            //controller.ChangeState(PlayerState.Idle);
        }
    }
}
