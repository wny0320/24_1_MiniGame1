using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BaseState
{
    Transform playerTrans;
    float moveSpeed = 3f;

    public BossMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;

    }


    public override void OnStateUpdate()
    {
        Vector3 playerDir = Vector3.Normalize(playerTrans.position); // ¡§±‘»≠ ∫§≈Õ
        rb.transform.position = playerDir * Time.deltaTime * moveSpeed;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
