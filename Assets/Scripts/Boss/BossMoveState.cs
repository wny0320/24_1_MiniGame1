using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BaseState
{
    const string BOSS_MOVE = "isMoving";
    BossController bossController;
    bool pattern0Flag = false;
    Transform transform;
    public BossMoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null, Transform transform = null)
        : base(controller, rb, animator, stat)
    {
        this.transform = transform;
    }

    public override void OnStateEnter()
    {
        animator.SetBool(BOSS_MOVE, true);
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
        animator.SetBool(BOSS_MOVE, false);
    }
    public void BossMove()
    {
        if (bossController == null)
            return;
        if (bossController.playerTrans == null)
            bossController.playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)bossController.playerTrans.position - rb.position);
        //Debug.Log(playerTrans.position);
        if(pattern0Flag == true)
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed * 2.5f;
        else
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed;
    }
    public void BossPatternTimeFunc()
    {
        bossController.patternTimer += Time.deltaTime;
        float playerDis = Vector3.Magnitude(bossController.playerTrans.position - rb.transform.position);
        if (bossController.patternTimer >= bossController.patternTime)
        {
            if (playerDis > bossController.unitDis * 15 && Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                pattern0Flag = true;
                bossController.patternTimer -= 1f;
            }
            else
            {
                pattern0Flag = false;
                controller.ChangeState(BossState.Pattern);
            }
            //아래는 거리 상관없이 랜덤으로 구현했던것
            //if(pattern0Flag == false)
            //{
            //    if (Random.Range(0.0f, 1.0f) >= 0.5f)
            //    {
            //        pattern0Flag = false;
            //        controller.ChangeState(BossState.Pattern);
            //    }
            //    else
            //    {
            //        pattern0Flag = true;
            //        //임의의 시간으로 빠르게 달려가는것을 구현
            //        bossController.patternTimer -= 1f;
            //    }
            //}
        }
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
    private Vector3 BossClipping()
    {
        /// <summary>
        /// 보스 클리핑
        /// </summary>
        /// <returns>Vector3, player의 clipping된 좌표</returns>

        if (controller.ground == null) return Vector3.zero;

        Transform groundTrans = controller.ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        // 스케일이 아닌 스프라이트 사이즈 고려를 해야할듯?
        Vector3 playerPos = transform.position;
        Vector3 playerScale = transform.lossyScale;

        // 클립핑할 사각형의 왼쪽 아래점과 오른쪽 위의 점을 구함
        Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + playerScale / 2;
        Vector2 clipSquareRightUp = groundPos + groundScale / 2 - playerScale / 2;

        float clipedXPos = Mathf.Clamp(playerPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(playerPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
    }
}
