using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : BaseState
{
    Collider2D col = null;

    public PlayerDodgeState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Collider2D collider = null)
        : base(controller, rb, animator)
    {
        col = collider;
    }

    public override void OnStateEnter()
    {
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
