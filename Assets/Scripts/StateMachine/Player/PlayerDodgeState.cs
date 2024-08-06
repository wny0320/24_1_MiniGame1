using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerDodgeState : BaseState
{
    PlayerController pc = null;

    const string PLAYER_DODGING = "isDodging";
    const string X_DIR = "xDir";
    const string Y_DIR = "yDir";

    Collider2D col = null;

    private Vector2 dir;

    private float dodgingTime = 0.5f;
    private float time = 0f;

    public PlayerDodgeState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Collider2D collider = null)
        : base(controller, rb, animator)
    {
        pc = controller as PlayerController;

        col = collider;
        Manager.Input.PlayerDodge -= PlayerDodge;
        Manager.Input.PlayerDodge += PlayerDodge;
    }

    public override void OnStateEnter()
    {
        time = 0f;
        col.enabled = false;
        rb.AddForce(dir*5, ForceMode2D.Impulse);
        animator.SetBool(PLAYER_DODGING, true);
    }


    public override void OnStateUpdate()
    {
        animator.SetFloat(X_DIR, dir.x);
        animator.SetFloat(Y_DIR, dir.y);

        if (time < dodgingTime) time += Time.deltaTime;
        else controller.ChangeState(PlayerState.Move);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
        rb.velocity = Vector2.zero;
        pc.isDodging = false;
        col.enabled = true;
        animator.SetBool(PLAYER_DODGING, false);
    }

    private void PlayerDodge(Vector2 dir)
    {
        if (pc.isDodging) { Debug.Log("isDodging"); return; }

        this.dir = dir;
        pc.isDodging = true;
        controller.ChangeState(PlayerState.Dodge);
    }
}
