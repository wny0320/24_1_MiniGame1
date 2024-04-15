using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : BaseState
{
    Collider2D col = null;

    private Vector2 dir;

    private float dodgingTime = 0.5f;
    private float time = 0f;

    public PlayerDodgeState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Collider2D collider = null)
        : base(controller, rb, animator)
    {
        col = collider;
        Manager.Input.PlayerDodge -= PlayerDodge;
        Manager.Input.PlayerDodge += PlayerDodge;
    }

    public override void OnStateEnter()
    {
        Debug.Log(dir);

        time = 0f;
        col.enabled = false;
        rb.AddForce(dir*5, ForceMode2D.Impulse);
    }


    public override void OnStateUpdate()
    {
        if (time < dodgingTime) time += Time.deltaTime;
        else controller.ChangeState(PlayerState.Move);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
        rb.velocity = Vector2.zero;
        Manager.Input.isDodging = false;
        col.enabled = true;
    }

    private void PlayerDodge(Vector2 dir)
    {
        if (Manager.Input.isDodging) { Debug.Log("isDodging"); return; }

        this.dir = dir;
        Manager.Input.isDodging = true;
        controller.ChangeState(PlayerState.Dodge);
    }
}
