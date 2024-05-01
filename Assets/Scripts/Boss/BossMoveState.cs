using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BossMoveState : BaseState
{
    Transform playerTrans;
    float moveSpeed = 1f;
    float patternStartTime = 5f;
    float patternTimer = 0;

    public BossMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {
        // ���� ������ ������ ���·� ������ ���, ���� Ÿ�̸Ӹ� �ٽ� �ʱⰪ���� ����
        patternTimer = 0;
    }


    public override void OnStateUpdate()
    {
        BossMove();
        BossPatternTimeFunc();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    public void BossMove()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
        //Debug.Log(playerTrans.position);
        rb.transform.position += playerDir * Time.deltaTime * moveSpeed;
    }
    public void BossPatternTimeFunc()
    {
        patternTimer += Time.deltaTime;
        if (patternTimer >= patternStartTime)
            controller.ChangeState(BossState.Pattern);
    }
}
