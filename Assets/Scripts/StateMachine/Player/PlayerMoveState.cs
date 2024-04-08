using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    public PlayerMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.left * Time.deltaTime * 10;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
