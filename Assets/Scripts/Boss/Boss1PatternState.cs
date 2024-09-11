using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class Boss1PatternState : BaseState
{
    // 패턴 코루틴들의 리스트
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
        //플레이어의 Transform을 받아오지 못할 경우 return
        if (Manager.Game.Player.transform == null)
            return;
        //실행 중인 패턴이 있을 경우 return
        if (Manager.Instance.nowPattern != null)
            return;
        //패턴 리스트에 패턴이 없다면 return
        if (patternList.Count < 1)
            return;
        Debug.Log("Pattern Selecting...");
        Transform playerTrans = Manager.Game.Player.transform;
        float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rb.position);
        // 패턴마다 조건이 충족되었는지 담고 있는 트리거 어레이
        // 기본 배열 생성시 bool 값은 false
        bool[] patternTrigger = new bool[patternList.Count+1];
        #region 패턴 조건 조건문
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
        #region 랜덤 패턴 State Change
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
        // controller.ChangeState(BossState.Move); 이건 패턴들 뒤에 넣어야할듯?
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
        //해당 타입에서 모든 메소드를 가져오는 코드
        //https://stackoverflow.com/questions/1198417/generate-list-of-methods-of-a-class-with-method-types
        //foreach (var method in typeof(Boss1PatternState).GetMethods())
        //{
        //    var parameters = method.GetParameters();
        //    var parameterDescriptions = parameters.Select(x => x.ParameterType + " " + x.Name).ToArray();
        //}
        //코루틴을 가져와서 실행하는 자료
        //https://discussions.unity.com/t/get-ienumerator-from-name-or-convert-methodinfo-to-ienumerator/155580/2
    }
    #region 패턴
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

        // 4번 반복
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
        // 검을 찌를 준비 자세를 취함
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
        // 병사 3명 소환, 병사, 화살 prefab 필요
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
        // 0.8초간 병사 활시위 당김
        yield return new WaitForSeconds(0.8f);
        // 4초간 플레이어 조준
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
        // 이후 플레이어 위치로 화살 발사
        // 화살 발사
        int cnt = zzol.Length;
        for (int i = 0; i < cnt; i++)
        {
            Vector3 dir = Vector3.Normalize(targetPos - zzol[i].transform.position);
            zzol[i].GetComponent<Zzol>()?.ShootSet(dir);
        }
        // 맞았다면 데미지, zzol에서 처리
        bossController.patternTimer = 0;
        animator.SetBool(BOSS_PATTERN3, false);
        controller.ChangeState(BossState.Move);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern4()
    {
        Debug.Log("Pattern4 Invoke");
        animator.SetBool(BOSS_PATTERN4, true);
        // 땅에 칼을 박음
        // 1.5초 뒤 점프
        yield return new WaitForSeconds(1.5f);
        // 점프
        float bossJumpTime = 1f;
        float bossJumpTimer = 0f;
        while(bossJumpTimer <= bossJumpTime)
        {
            rb.transform.position += new Vector3(0, 20, 0) * Time.deltaTime;
            bossJumpTimer += Time.deltaTime;
            yield return null;
        }
        // 2초간 반복
        float bossTrackTime = 2f;
        float bossTrackTimer = 0f;
        // 플레이어의 이속을 받아오기 위해 스탯을 가져옴
        Stat playerStat = Manager.Game.Player.GetComponent<Stat>();
        Vector3 targetPos = Manager.Game.Player.GetComponent<Transform>().position;
        Debug.Log(playerStat);
        GameObject range = GameObject.Instantiate(bossController.pattern4Range);
        while (bossTrackTimer <= bossTrackTime)
        {
            Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
            // 갱신된 플레이어의 위치로 이동
            targetPos =  Vector3.MoveTowards(targetPos, playerPos, playerStat.MoveSpeed * 0.9f * Time.deltaTime);
            // targetPos 위치로 원 이동
            range.transform.position = targetPos;
            yield return bossTrackTimer += Time.deltaTime;
        }
        rb.transform.position = new Vector3(targetPos.x, targetPos.y + 20f, targetPos.z);
        // 플레이어 위치로 내려찍음, 자세한 속도와 그런건 정해진게 없어서 그냥 하면 될듯?
        float bossDownTime = 1f;
        float bossDownTimer = 0f;
        // 내려찍음
        while(bossDownTimer <= bossDownTime)
        {
            rb.transform.position -= new Vector3(0, 20, 0) * Time.deltaTime;
            bossDownTimer += Time.deltaTime;
            yield return null;
        }
        // 3 단위미터 내 원형으로 데미지를 줌
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
    //    // 3초에 걸려서 체력 100을 회복, 받는 데미지 30% 감소
    //    int healAmount = 100;
    //    float healTimer = 3f;
    //    float healTime = 0f;
    //    while (true)
    //    {
    //        if (healTime > healTimer)
    //            break;
    //        // 받는 데미지 30% 감소
    //        bossController.reduceDamageFlag = true;
    //        // 3초에 따른 호출당 피회복
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
    #region 과거 패턴 함수들
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
    //            //대검 찍기
    //        }
    //    });
    //    //스테이트 바꾸기
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
    //    //대검 찌르기
    //    await new Task(() =>
    //    {
    //        while (timer < time)
    //        {
    //            timer += Time.deltaTime;
    //            rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 4f;
    //        }
    //    });
    //    //후딜레이
    //    await new Task(() =>
    //    {
    //        new WaitForSeconds(3f);
    //    });
    //    //스테이트 바꾸기
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern3()
    //{
    //    Debug.Log("Pattern3 Invoked");
    //    float timer = 0f;
    //    float time = 4f;
    //    // 병사 소환
    //    await new Task(() =>
    //    {
    //        Vector3 realPlayerPos = playerTrans.position;
    //        new WaitForSeconds(0.8f);
    //        while (timer < time)
    //        {
    //            //병사 조준
    //        }
    //        //화살 발사
    //    });
    //    //스테이트 바꾸기
    //    controller.ChangeState(BossState.Move);
    //}
    //public async void Pattern4()
    //{
    //    Debug.Log("Pattern4 Invoked");
    //    await new Task(() =>
    //    {
    //        //땅에 칼 찍기
    //        new WaitForSeconds(1.5f);
    //        float timer = 0f;
    //        float time = 1f;
    //        //점프
    //        while (timer < time)
    //        {
    //            rb.transform.position += Vector3.up * Time.deltaTime;
    //        }
    //        //내려찍기
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
    //        // 100회복할때까지 회복
    //        // 30% 뎀감
    //    });
    //}
    #endregion
}
