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
        
    }


    public override void OnStateUpdate()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
        //Debug.Log(playerTrans.position);
        rb.transform.position += playerDir * Time.deltaTime * moveSpeed;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
