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
        // ���� ������ ������ ���·� ������ ���, ���� Ÿ�̸Ӹ� �ٽ� �ʱⰪ���� ����
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
        //���� ������ ���� ����ǰ� �ִٸ� ���� �ð� ���� ������Ű�� ����
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
        // ���� ��Ʈ�ѷ��� Ÿ�� ���� �Լ��� �۵��� ��쿣 �ð��� �ѹ� ���ش�
        if (bossController.timeAddFlag)
        {
            bossController.patternTime -= bossController.timeAdd;
            bossController.timeAddFlag = false;
        }
        // 1������ �� ��� ���� ���� Ÿ�� ��, 2������ �� ��� ���� Ÿ�� ������ ���ؼ� ��
        if ((!bossController.phase2Flag && bossController.patternTimer >= bossController.patternTime) || 
            (bossController.phase2Flag && bossController.patternTimer/ bossController.phase2TimeMul >= bossController.patternTime)) // ���� �ڵ�� �������� �뽬 ����
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
            //�Ʒ��� �Ÿ� ������� �������� �����ߴ���
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
            //        //������ �ð����� ������ �޷����°��� ����
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
