using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BaseState
{
    BossController bossController;
    public BossMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {
        bossController = controller as BossController;
        // 패턴 끝나고 움직임 상태로 들어오는 경우, 패턴 타이머를 다시 초기값으로 설정
        bossController.patternTimer = 0;
    }


    public override void OnStateUpdate()
    {
        GetBossController();
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
        if (bossController == null)
            return;
        if (bossController.playerTrans == null)
            bossController.playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)bossController.playerTrans.position - rb.position);
        //Debug.Log(playerTrans.position);
        rb.transform.position += playerDir * Time.deltaTime * bossController.moveSpeed;
    }
    public void BossPatternTimeFunc()
    {
        bossController.patternTimer += Time.deltaTime;
        if (bossController.patternTimer >= bossController.patternTime)
            controller.ChangeState(BossState.Pattern);
    }
    public void GetBossController()
    {
        if(bossController == null)
        {
            if (controller == null)
                return;
            else
                bossController = controller as BossController;
        }
        return;
    }
}
