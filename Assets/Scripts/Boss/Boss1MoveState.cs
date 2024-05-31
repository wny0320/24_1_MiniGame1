using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1MoveState : BaseState
{
    const string BOSS_MOVE = "isMoving";
    BossController bossController;
    bool pattern0Flag = false;
    public Boss1MoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null, Transform transform = null)
        : base(controller, rb, animator, stat)
    {

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
        if (Manager.Game.Player.transform == null)
            return;
        Transform playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
        //Debug.Log(playerTrans.position);
        if(pattern0Flag == true)
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed * 2.5f;
        else
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed;
        rb.transform.position = BossClipping();
    }
    public void BossPatternTimeFunc()
    {
        //현재 패턴이 아직 진행되고 있다면 패턴 시간 값을 증가시키지 않음
        if (Manager.Instance.nowPattern != null)
            return;
        bossController.patternTimer += Time.deltaTime;
        Transform playerTrans = Manager.Game.Player.transform;
        float playerDis = Vector3.Magnitude(playerTrans.position - rb.transform.position);
        // 보스 컨트롤러의 타임 에더 함수가 작동된 경우엔 시간을 한번 빼준다
        if (bossController.timeAddFlag)
        {
            bossController.patternTime -= bossController.timeAdd;
            bossController.timeAddFlag = false;
        }
        // 1페이즈 일 경우 기존 패턴 타임 비교, 2페이즈 일 경우 패턴 타임 배율을 곱해서 비교
        if ((!bossController.phase2Flag && bossController.patternTimer >= bossController.patternTime) || 
            (bossController.phase2Flag && bossController.patternTimer/ bossController.phase2TimeMul >= bossController.patternTime)) // 현재 코드는 연속으로 대쉬 가능
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
        
        Vector3 spriteSize = rb.GetComponent<SpriteRenderer>().sprite.rect.size;
        Debug.Log(spriteSize);
        if (controller.ground == null) return Vector3.zero;

        Transform groundTrans = controller.ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        // 스케일이 아닌 스프라이트 사이즈 고려를 해야할듯?
        Vector3 bossPos = rb.transform.position;
        Vector3 bossScale = rb.transform.lossyScale;

        // 클립핑할 사각형의 왼쪽 아래점과 오른쪽 위의 점을 구하고 플레이어 rect 사이즈 고려
        // 아래 코드는 스케일로 작성
        //Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + bossScale / 2;
        //Vector2 clipSquareRightUp = groundPos + groundScale / 2 - bossScale / 2;

        Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + spriteSize / 2;
        Vector2 clipSquareRightUp = groundPos + groundScale / 2 - spriteSize / 2;
        float clipedXPos = Mathf.Clamp(bossPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(bossPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
    }
}
