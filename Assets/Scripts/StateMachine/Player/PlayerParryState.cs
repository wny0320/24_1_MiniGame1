using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class PlayerParryState : BaseState
{
    PlayerController pc = null;
    private float parryingTime = 0.5f;
    private float time = 0f;
    

    public PlayerParryState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {
        pc = controller as PlayerController;
    }

    public override void OnStateEnter()
    {
        pc.isParrying = true;
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time > parryingTime)
        {
            time = 0f;
            controller.ChangeState(PlayerState.Move);
        }
    }

    public override void OnStateExit()
    {
        pc.isParrying = false;
    }
}
