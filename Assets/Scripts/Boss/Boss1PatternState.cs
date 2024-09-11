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
    Stat bossStat;
    public const string BOSS_PATTERN1 = "Pattern1Flag";
    public const string BOSS_PATTERN2 = "Pattern2Flag";
    public const string BOSS_PATTERN3 = "Pattern3Flag";
    public const string BOSS_PATTERN4 = "Pattern4Flag";
    public const string BOSS_PATTERN5 = "Pattern5Flag";
    public Boss1PatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {
        GetCoroutineList();
        GetBossData();
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
    private void GetBossData()
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
        //���� ����Ʈ�� ������ ���ٸ� return
        if (patternList.Count < 1)
            return;
        Debug.Log("Pattern Selecting...");
        Transform playerTrans = Manager.Game.Player.transform;
        float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rb.position);
        // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
        // �⺻ �迭 ������ bool ���� false
        bool[] patternTrigger = new bool[patternList.Count+1];
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
        //if (bossController.phase2Flag && controller.stat.Hp <= 20f && bossController.noDamagedTime >= 8f)
        //    patternTrigger[5] = true;
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
        //MethodInfo targetPattern = patternList[selectedIndex];
        MethodInfo targetPattern = patternList[3];
        Debug.Log("Selected Pattern = " + targetPattern);
        Manager.Instance.InvokePattern(this, targetPattern);
        // controller.ChangeState(BossState.Move); �̰� ���ϵ� �ڿ� �־���ҵ�?
        #endregion
    }
    public void GetCoroutineList()
    {
        Debug.Log("GetCoroutineList...");
        if (patternList.Count > 0)
            return;
        foreach(var method in typeof(Boss1PatternState).GetMethods())
        {
            if(method.ReturnType == typeof(IEnumerator))
            {
                patternList.Add(method);
            }
        }
        Debug.Log("Boss Pattern Coroutine Count = " + patternList.Count);
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
        List<Collider2D> hitted = new();
        Debug.Log("Pattern1 Invoke");
        animator.SetBool(BOSS_PATTERN1, true);
        Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
        yield return new WaitForSeconds(1f);
        float t = 0f;
        float xVal = animator.GetFloat("xDir");
        float yVal = animator.GetFloat("yDir");

        // 4�� �ݺ�
        for (int _attackNum = 0; _attackNum < 4; _attackNum++)
        {
            Debug.Log("Attack Start");
            while (t <= (1f / 6f * 2f))
            {
                Collider2D[] cols;
                t += Time.deltaTime;
                yield return null;
                if (xVal == 0 && yVal == 1) //up
                    cols = Physics2D.OverlapBoxAll(rb.position + Vector2.up, Vector2.one * 2, 0f, 1 << LayerMask.NameToLayer("Player"));
                else if (xVal == 0 && yVal == -1) //d
                    cols = Physics2D.OverlapBoxAll(rb.position + Vector2.down, Vector2.one * 2, 0f, 1 << LayerMask.NameToLayer("Player"));
                else if (xVal == 1 && yVal == 0) //ri
                    cols = Physics2D.OverlapBoxAll(rb.position + Vector2.right * 1.5f, new Vector2(2, 3), 0f, 1 << LayerMask.NameToLayer("Player"));
                else //le
                    cols = Physics2D.OverlapBoxAll(rb.position + Vector2.left * 1.5f, new Vector2(2, 3), 0f, 1 << LayerMask.NameToLayer("Player"));

                foreach (Collider2D col in cols)
                {
                    if (!hitted.Contains(col))
                    {
                        hitted.Add(col);
                        col.GetComponent<IReceiveAttack>()?.OnHit(20);
                    }
                }
            }
            Debug.Log("Attack End");
            hitted.Clear();
            t = 0f;
            yield return new WaitForSeconds(1f / 6f);
        }
        Debug.Log("loop out");
        animator.SetBool(BOSS_PATTERN1, false);
        controller.ChangeState(BossState.Move);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern2()
    {
        List<Collider2D> hitted = new();
        Debug.Log("Pattern2 Invoke");
        animator.SetBool(BOSS_PATTERN2, true);
        // ���� � �غ� �ڼ��� ����
        yield return new WaitForSeconds(1.25f);
        Vector3 dir = (Manager.Game.Player.GetComponent<Transform>().position - (Vector3)rb.position).normalized;

        float t = 0f;
        float xVal = animator.GetFloat("xDir");
        float yVal = animator.GetFloat("yDir");
        while (t <= 0.5f)
        {
            Collider2D[] cols;
            t += Time.deltaTime;
            yield return null;
            if (xVal == 0 && yVal == 1) //up
                cols = Physics2D.OverlapBoxAll(rb.position + Vector2.up, Vector2.one * 2, 0f, 1 << LayerMask.NameToLayer("Player"));
            else if (xVal == 0 && yVal == -1) //d
                cols = Physics2D.OverlapBoxAll(rb.position + Vector2.down, Vector2.one * 2, 0f, 1 << LayerMask.NameToLayer("Player"));
            else if (xVal == 1 && yVal == 0) //ri
                cols = Physics2D.OverlapBoxAll(rb.position + Vector2.right * 1.5f, new Vector2(3, 2), 0f, 1 << LayerMask.NameToLayer("Player"));
            else //le
                cols = Physics2D.OverlapBoxAll(rb.position + Vector2.left * 1.5f, new Vector2(3, 2), 0f, 1 << LayerMask.NameToLayer("Player"));

            rb.MovePosition(dir * 0.5f + (Vector3)rb.position);

            foreach (Collider2D col in cols)
            {
                if (!hitted.Contains(col))
                {
                    hitted.Add(col);
                    col.GetComponent<IReceiveAttack>()?.OnHit(80);
                }
            }
        }

        bossController.PatternTimeAdder(3f);
        controller.ChangeState(BossState.Move);
        bossController.patternTimer = 0;
        animator.SetBool(BOSS_PATTERN2, false);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern3()
    {
        Debug.Log("Pattern3 Invoke");
        animator.SetBool(BOSS_PATTERN3, true);
        yield return null;
        // ���� 3�� ��ȯ, ����, ȭ�� prefab �ʿ�
        GameObject[] zzol = new GameObject[3];
        GameObject[] zzolRange = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            zzol[i] = GameObject.Instantiate(bossController.zzol);
            zzolRange[i] = GameObject.Instantiate(zzol[i].GetComponent<Zzol>()?.zzolRangeObj);
            switch(i)
            {
                case 0:
                    zzol[i].transform.position = new Vector3(rb.transform.position.x - 1f, rb.transform.position.y, rb.transform.position.z);
                    break;
                case 1:
                    zzol[i].transform.position = new Vector3(rb.transform.position.x + 1f, rb.transform.position.y, rb.transform.position.z);
                    break;
                case 2:
                    zzol[i].transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - 1f, rb.transform.position.z);
                    break;
                default:
                    break;
            }
            zzolRange[i].transform.position = zzol[i].transform.position;
        }
        // 0.8�ʰ� ���� Ȱ���� ���
        yield return new WaitForSeconds(0.8f);
        // 4�ʰ� �÷��̾� ����
        float ArrowTime = 4f;
        float ArrowTimer = 0f;
        Transform playerTrans = Manager.Game.Player.transform;
        Vector3 targetPos = Vector3.zero;
        while(ArrowTimer <= ArrowTime)
        {
            foreach (var range in zzolRange)
            {
                Vector2 dir = range.transform.position - playerTrans.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                Quaternion rotation = Quaternion.Slerp(angleAxis, range.transform.rotation, Time.deltaTime * 7f);
                Debug.Log(angle + ", " + rotation);
                range.transform.rotation = rotation;
            }
            ArrowTimer += Time.deltaTime;
            targetPos = playerTrans.position;
            yield return null;
        }
        foreach (var range in zzolRange)
        {
            GameObject.Destroy(range.gameObject);
        }
        // ���� �÷��̾� ��ġ�� ȭ�� �߻�
        // ȭ�� �߻�
        int cnt = zzol.Length;
        for (int i = 0; i < cnt; i++)
        {
            Vector3 dir = Vector3.Normalize(targetPos - zzol[i].transform.position);
            zzol[i].GetComponent<Zzol>()?.ShootSet(dir);
        }
        // �¾Ҵٸ� ������, zzol���� ó��
        bossController.patternTimer = 0;
        animator.SetBool(BOSS_PATTERN3, false);
        controller.ChangeState(BossState.Move);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern4()
    {
        Debug.Log("Pattern4 Invoke");
        animator.SetBool(BOSS_PATTERN4, true);
        // ���� Į�� ����
        // 1.5�� �� ����
        yield return new WaitForSeconds(1.5f);
        // ����
        float bossJumpTime = 1f;
        float bossJumpTimer = 0f;
        while(bossJumpTimer <= bossJumpTime)
        {
            rb.transform.position += new Vector3(0, 20, 0) * Time.deltaTime;
            bossJumpTimer += Time.deltaTime;
            yield return null;
        }
        // 2�ʰ� �ݺ�
        float bossTrackTime = 2f;
        float bossTrackTimer = 0f;
        // �÷��̾��� �̼��� �޾ƿ��� ���� ������ ������
        Stat playerStat = Manager.Game.Player.GetComponent<Stat>();
        Vector3 targetPos = Manager.Game.Player.GetComponent<Transform>().position;
        Debug.Log(playerStat);
        GameObject range = GameObject.Instantiate(bossController.pattern4Range);
        while (bossTrackTimer <= bossTrackTime)
        {
            Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
            // ���ŵ� �÷��̾��� ��ġ�� �̵�
            targetPos =  Vector3.MoveTowards(targetPos, playerPos, playerStat.MoveSpeed * 0.9f * Time.deltaTime);
            // targetPos ��ġ�� �� �̵�
            range.transform.position = targetPos;
            yield return bossTrackTimer += Time.deltaTime;
        }
        rb.transform.position = new Vector3(targetPos.x, targetPos.y + 20f, targetPos.z);
        // �÷��̾� ��ġ�� ��������, �ڼ��� �ӵ��� �׷��� �������� ��� �׳� �ϸ� �ɵ�?
        float bossDownTime = 1f;
        float bossDownTimer = 0f;
        // ��������
        while(bossDownTimer <= bossDownTime)
        {
            rb.transform.position -= new Vector3(0, 20, 0) * Time.deltaTime;
            bossDownTimer += Time.deltaTime;
            yield return null;
        }
        // 3 �������� �� �������� �������� ��
        Collider2D[] cols;
        cols = Physics2D.OverlapCircleAll(rb.transform.position, bossController.unitDis * 3f, 1 << LayerMask.NameToLayer("Player"));
        foreach(Collider2D hit in cols)
            hit.GetComponent<IReceiveAttack>()?.OnHit(100);
        GameObject.Destroy(range);
        bossController.patternTimer = 0;
        animator.SetBool(BOSS_PATTERN4 , false);
        controller.ChangeState(BossState.Move);
        yield return Manager.Instance.nowPattern = null;
    }
    //public IEnumerator Pattern5()
    //{
    //    Debug.Log("Pattern5 Invoke");
    //    animator.SetBool (BOSS_PATTERN5, true);
    //    // 3�ʿ� �ɷ��� ü�� 100�� ȸ��, �޴� ������ 30% ����
    //    int healAmount = 100;
    //    float healTimer = 3f;
    //    float healTime = 0f;
    //    while (true)
    //    {
    //        if (healTime > healTimer)
    //            break;
    //        // �޴� ������ 30% ����
    //        bossController.reduceDamageFlag = true;
    //        // 3�ʿ� ���� ȣ��� ��ȸ��
    //        bossController.stat.Hp += healAmount/healTimer * Time.deltaTime;
    //        yield return healTime += Time.deltaTime;
    //    }
    //    bossController.patternTimer = 0;
    //    bossController.reduceDamageFlag=false;
    //    animator.SetBool(BOSS_PATTERN5 , false);
    //    controller.ChangeState(BossState.Move);
    //    yield return Manager.Instance.nowPattern = null;
    //}
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
