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
        //���� ������ ���� ����ǰ� �ִٸ� ���� �ð� ���� ������Ű�� ����
        if (Manager.Instance.nowPattern != null)
            return;
        bossController.patternTimer += Time.deltaTime;
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
    private Vector3 BossClipping()
    {
        /// <summary>
        /// ���� Ŭ����
        /// </summary>
        /// <returns>Vector3, player�� clipping�� ��ǥ</returns>
        
        Vector3 spriteSize = rb.GetComponent<SpriteRenderer>().sprite.rect.size;
        Debug.Log(spriteSize);
        if (controller.ground == null) return Vector3.zero;

        Transform groundTrans = controller.ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        // �������� �ƴ� ��������Ʈ ������ ����� �ؾ��ҵ�?
        Vector3 bossPos = rb.transform.position;
        Vector3 bossScale = rb.transform.lossyScale;

        // Ŭ������ �簢���� ���� �Ʒ����� ������ ���� ���� ���ϰ� �÷��̾� rect ������ ���
        // �Ʒ� �ڵ�� �����Ϸ� �ۼ�
        //Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + bossScale / 2;
        //Vector2 clipSquareRightUp = groundPos + groundScale / 2 - bossScale / 2;

        Vector2 clipSquareLeftDown = groundPos - groundScale / 2 + spriteSize / 2;
        Vector2 clipSquareRightUp = groundPos + groundScale / 2 - spriteSize / 2;
        float clipedXPos = Mathf.Clamp(bossPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(bossPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
    }
}
