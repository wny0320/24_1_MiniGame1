using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1MoveState : BaseState
{
    const string BOSS_MOVE = "MoveFlag";
    BossController bossController;
    bool pattern0Flag = false;
    float originPatternTime = float.NegativeInfinity;
    public Boss1MoveState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null, Transform transform = null)
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
        if (Manager.Game.Player.transform == null)
            return;
        Debug.Log("BossMove Invoked");
        Transform playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
        Vector3 playerDis = (Vector2)playerTrans.position - rb.position;
        if (playerDis.magnitude < 2f)
        {
            animator.SetBool(BOSS_MOVE, false);
            return;
        }
        Debug.Log("Boss Move On");
        animator.SetBool(BOSS_MOVE, true);
        if(pattern0Flag == true)
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed * 2.5f;
        else
            rb.transform.position += playerDir * Time.deltaTime * bossController.stat.MoveSpeed;
        //rb.transform.position = BossClipping();
    }
    public void BossPatternTimeFunc()
    {
        //현재 패턴이 아직 진행되고 있다면 패턴 시간 값을 증가시키지 않음
        if (Manager.Instance.nowPattern != null)
            return;
        if (originPatternTime < 0)
            originPatternTime = bossController.patternTime;
        if (bossController.patternTime != originPatternTime)
            bossController.patternTime = originPatternTime;
        bossController.patternTimer += Time.deltaTime;
        Debug.Log("Timer Works");
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
                Debug.Log("pattern time = " + bossController.patternTimer);
                animator.SetBool(BOSS_MOVE, false);
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
}
