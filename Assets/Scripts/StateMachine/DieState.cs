using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    public DieState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Transform transform = null)
        : base(controller, rb, animator)
    {
    }

    public override void OnStateEnter()
    {
        animator.Play("Die");
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
