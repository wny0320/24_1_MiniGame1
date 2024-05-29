using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class Boss1PatternState : BaseState
{
    // ���� �ڷ�ƾ���� ����Ʈ
    public List<Coroutine> patternList = new List<Coroutine>();
    BossController bossController;
    public Boss1PatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {
        //MethodBase.
    }


    public override void OnStateUpdate()
    {
        GetBossController();
        PatternSelect();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    private void PatternSelect()
    {
        if (bossController == null)
            return;
        if (Manager.Game.Player.transform == null)
            return;
        Transform playerTrans = Manager.Game.Player.transform;
        float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rb.position);
        // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
        bool[] patternTrigger = new bool[patternList.Count];
        #region ���� ���� ���ǹ�
        patternTrigger[0] = false;
        if (playerDis < Manager.Pattern.unitDis * 1.3f)
            patternTrigger[1] = true;
        if (playerDis < Manager.Pattern.unitDis * 8f)
            patternTrigger[2] = true;
        if (playerDis > Manager.Pattern.unitDis * 12f)
            patternTrigger[3] = true;
        if (playerDis < Manager.Pattern.unitDis * 20f)
            patternTrigger[4] = true;
        //ü�� ���� ���� ����
        #endregion
        #region ���� ���� State Change
        List<int> activeTriggerList = new List<int>();
        for (int i = 0; i < patternList.Count; i++)
        {
            if (patternTrigger[i] == true)
                activeTriggerList.Add(i);
        }
        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
        int selectedIndex = activeTriggerList[n];
        string patternName = "Pattern" + selectedIndex;
        Boss1Pattern targetPattern = (Boss1Pattern)Enum.Parse(typeof(Boss1Pattern), patternName);
        //���� �������� �ʾ����Ƿ� Move���·� ������
        //controller.ChangeState(targetPattern);
        controller.ChangeState(BossState.Move);
        #endregion
    }
    public void GetBossController()
    {
        if (bossController == null)
        {
            if (controller == null)
                return;
            else
                bossController = controller as BossController;
        }
        return;
    }
    public IEnumerator Pattern1()
    {
        yield return null;
    }
    #region ���� ���� �Լ���
    //public async void Pattern1()
    //{
    //    Debug.Log("Pattern1 Invoked");
    //    float time = 0.8f;
    //    int number = 5;
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(1f);
    //    });
    //    await new Task(() =>
    //    {
    //        for (int i = 0; i < number; i++)
    //        {
    //            new WaitForSeconds(time);
    //            //��� ���
    //        }
    //    });
    //    //������Ʈ �ٲٱ�
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern2()
    //{
    //    Debug.Log("Pattern2 Invoked");
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(1.3f);
    //    });
    //    float timer = 0f;
    //    float time = 1f;
    //    //��� ���
    //    await new Task(() =>
    //    {
    //        while (timer < time)
    //        {
    //            timer += Time.deltaTime;
    //            rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 4f;
    //        }
    //    });
    //    //�ĵ�����
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(3f);
    //    });
    //    //������Ʈ �ٲٱ�
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern3()
    //{
    //    Debug.Log("Pattern3 Invoked");
    //    float timer = 0f;
    //    float time = 4f;
    //    // ���� ��ȯ
    //    await new Task(() =>
    //    {
    //        Vector3 realPlayerPos = playerTrans.position;
    //        new WaitForSeconds(0.8f);
    //        while (timer < time)
    //        {
    //            //���� ����
    //        }
    //        //ȭ�� �߻�
    //    });
    //    //������Ʈ �ٲٱ�
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern4()
    //{
    //    Debug.Log("Pattern4 Invoked");
    //    await new Task(() =>
    //    {
    //        //���� Į ���
    //        new WaitForSeconds(1.5f);
    //        float timer = 0f;
    //        float time = 1f;
    //        //����
    //        while (timer < time)
    //        {
    //            rb.transform.position += Vector3.up * Time.deltaTime;
    //        }
    //        //�������
    //        while (timer < time)
    //        {
    //            rb.transform.position += Vector3.down * Time.deltaTime;
    //        }
    //    });
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern5()
    //{
    //    Debug.Log("Pattern5 Invoked");
    //    await new Task(() =>
    //    {
    //        // 100ȸ���Ҷ����� ȸ��
    //        // 30% ����
    //    });
    //}
    #endregion
}
