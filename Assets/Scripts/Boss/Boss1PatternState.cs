using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class Boss1PatternState : BaseState
{
    // ���� �ڷ�ƾ���� ����Ʈ
    public List<MethodInfo> patternList = new List<MethodInfo>();
    BossController bossController;
    public Boss1PatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {
        GetCoroutineList();
        GetBossController();
    }


    public override void OnStateUpdate()
    {
        PatternSelect();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    private void GetBossController()
    {
        bossController = (BossController)Convert.ChangeType(controller, typeof(BossController));
    }
    private void PatternSelect()
    {
        //�÷��̾��� Transform�� �޾ƿ��� ���� ��� return
        if (Manager.Game.Player.transform == null)
            return;
        //���� ���� ������ ���� ��� return
        if (Manager.Instance.nowPattern != null)
            return;
        Transform playerTrans = Manager.Game.Player.transform;
        float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rb.position);
        // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
        // �⺻ �迭 ������ bool ���� false
        bool[] patternTrigger = new bool[patternList.Count];
        #region ���� ���� ���ǹ�
        patternTrigger[0] = false;
        if (playerDis < bossController.unitDis * 1.3f)
            patternTrigger[1] = true;
        if (playerDis < bossController.unitDis * 8f)
            patternTrigger[2] = true;
        if (playerDis > bossController.unitDis * 12f)
            patternTrigger[3] = true;
        if (playerDis < bossController.unitDis * 20f)
            patternTrigger[4] = true;
        if (bossController.phase2Flag && controller.stat.Hp <= 20f && bossController.noDamagedTime >= 8f)
            patternTrigger[5] = true;
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
        MethodInfo targetPattern = patternList[selectedIndex];
        Manager.Instance.InvokePattern(this, targetPattern);
        controller.ChangeState(BossState.Move);
        #endregion
    }
    public void GetCoroutineList()
    {
        foreach(var method in typeof(Boss1Pattern).GetMethods())
        {
            if(method.ReturnType == typeof(IEnumerator))
            {
                patternList.Add(method);
            }
        }
        //�ش� Ÿ�Կ��� ��� �޼ҵ带 �������� �ڵ�
        //https://stackoverflow.com/questions/1198417/generate-list-of-methods-of-a-class-with-method-types
        //foreach (var method in typeof(Boss1PatternState).GetMethods())
        //{
        //    var parameters = method.GetParameters();
        //    var parameterDescriptions = parameters.Select(x => x.ParameterType + " " + x.Name).ToArray();
        //}
        //�ڷ�ƾ�� �����ͼ� �����ϴ� �ڷ�
        //https://discussions.unity.com/t/get-ienumerator-from-name-or-convert-methodinfo-to-ienumerator/155580/2
    }
    #region ����
    public IEnumerator Pattern1()
    {
        // Vector3�� ����, ���� ������ �ű⼭ ����
        Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
        // �÷��̾����� �ٶ󺸸� ���� ġ�ѵ��
        yield return new WaitForSeconds(1f);
        // 5�� �ݺ�
        for (int _attackNum = 0; _attackNum < 5; _attackNum++)
        {
            // ���� �����ļ� ���� ���� �÷��̾ �ִٸ� �������� ����
            // ���⼭ �׷��ٸ� �÷��̾� Ž���� ������ ������ ������ ������
            // �ݶ��̴��� �Ұ��� Ž������ ���� �����ؾ��� 
            yield return new WaitForSeconds(0.8f);
        }
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern2()
    {
        // ���� � �غ� �ڼ��� ����
        yield return new WaitForSeconds(1.3f);
        // ���� 10�������� ��ŭ ������ ���ϸ� �̵�
        // ���� ���� �÷��̾ �ִٸ� �������� ����
        // ���� ���� �ֱ� 3�� ����
        bossController.PatternTimeAdder(3f);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern3()
    {
        // ���� 3�� ��ȯ, ����, ȭ�� prefab �ʿ�
        // 0.8�ʰ� ���� Ȱ���� ���
        // 4�ʰ� �÷��̾� ����
        // ���� �÷��̾� ��ġ�� ȭ�� �߻�
        Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
        // ȭ�� �߻�
        // �¾Ҵٸ� ������
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern4()
    {
        // ���� Į�� ����
        // 1.5�� �� ����
        yield return new WaitForSeconds(1.5f);
        // ����
        // 2�ʰ� ���� �������� ǥ��, �÷��̾ ����
        float downTimer = 2f;
        float downTime = 0f;
        // 2�ʰ� �ݺ�
        // �÷��̾��� �̼��� �޾ƿ��� ���� ������ ������
        Stat playerStat = Manager.Game.Player.GetComponent<Stat>();
        Vector3 targetPos = Manager.Game.Player.GetComponent<Transform>().position;
        while (true)
        {
            if (downTime > downTimer)
                break;
            Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
            // ���ŵ� �÷��̾��� ��ġ�� �̵�
            targetPos =  Vector3.MoveTowards(targetPos, playerPos, playerStat.MoveSpeed * 0.9f);
            // targetPos ��ġ�� �� �̵�
            yield return downTime += Time.deltaTime;
        }
        // �÷��̾� ��ġ�� ��������, �ڼ��� �ӵ��� �׷��� �������� ��� �׳� �ϸ� �ɵ�?
        // ��������
        // 3 �������� �� �������� �������� ��

        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern5()
    {
        // 3�ʿ� �ɷ��� ü�� 100�� ȸ��, �޴� ������ 30% ����
        int healAmount = 100;
        float healTimer = 3f;
        float healTime = 0f;
        while (true)
        {
            if (healTime > healTimer)
                break;
            // �޴� ������ 30% ����
            // 3�ʿ� ���� ȣ��� ��ȸ��
            bossController.stat.Hp += healAmount/healTimer * Time.deltaTime;
            yield return healTime += Time.deltaTime;
        }
        yield return Manager.Instance.nowPattern = null;
    }
    #endregion
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