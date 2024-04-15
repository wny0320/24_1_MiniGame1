using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        if (Manager.Input.isMoving) controller.ChangeState(PlayerState.Move);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
