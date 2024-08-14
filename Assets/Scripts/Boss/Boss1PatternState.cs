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
        Debug.Log("Pattern Selecting...");
        //플레이어의 Transform을 받아오지 못할 경우 return
        if (Manager.Game.Player.transform == null)
            return;
        //실행 중인 패턴이 있을 경우 return
        if (Manager.Instance.nowPattern != null)
            return;
        //패턴 리스트에 패턴이 없다면 return
        if (patternList.Count < 1)
            return;
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
        if (bossController.phase2Flag && controller.stat.Hp <= 20f && bossController.noDamagedTime >= 8f)
            patternTrigger[5] = true;
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
        MethodInfo targetPattern = patternList[selectedIndex];
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
        Debug.Log("Pattern1 Invoke");
        animator.SetBool(BOSS_PATTERN1, true);
        // Vector3는 값형, 값을 받으면 거기서 고정
        Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
        // 플레이어쪽을 바라보며 검을 치켜든다
        // 애니메이션을 정하는 코드로 넣으면 될듯
        yield return new WaitForSeconds(1f);
        // 5번 반복
        for (int _attackNum = 0; _attackNum < 5; _attackNum++)
        {
            // 검을 내려쳐서 범위 내의 플레이어가 있다면 데미지를 입힘
            // 여기서 그렇다면 플레이어 탐지와 데미지 입히는 로직이 들어가야함
            // 콜라이더로 할건지 탐색으로 할지 생각해야함 
            yield return new WaitForSeconds(0.8f);
        }
        controller.ChangeState(BossState.Move);
        animator.SetBool(BOSS_PATTERN1, false);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern2()
    {
        Debug.Log("Pattern2 Invoke");
        animator.SetBool(BOSS_PATTERN2, true);
        // 검을 찌를 준비 자세를 취함
        yield return new WaitForSeconds(1.3f);
        // 전방 10단위미터 만큼 공격을 가하며 이동
        // 범위 내에 플레이어가 있다면 데미지를 입힘
        // 다음 패턴 주기 3초 증가
        bossController.PatternTimeAdder(3f);
        controller.ChangeState(BossState.Move);
        animator.SetBool(BOSS_PATTERN2, false);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern3()
    {
        Debug.Log("Pattern3 Invoke");
        animator.SetBool(BOSS_PATTERN3, true);
        // 병사 3명 소환, 병사, 화살 prefab 필요
        // 0.8초간 병사 활시위 당김
        // 4초간 플레이어 조준
        // 이후 플레이어 위치로 화살 발사
        Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
        // 화살 발사
        // 맞았다면 데미지
        controller.ChangeState(BossState.Move);
        animator.SetBool(BOSS_PATTERN3, false);
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
        // 2초간 예상 낙하지점 표시, 플레이어를 따라감
        float downTimer = 2f;
        float downTime = 0f;
        // 2초간 반복
        // 플레이어의 이속을 받아오기 위해 스탯을 가져옴
        Stat playerStat = Manager.Game.Player.GetComponent<Stat>();
        Vector3 targetPos = Manager.Game.Player.GetComponent<Transform>().position;
        Debug.Log(playerStat);
        while (true)
        {
            if (downTime > downTimer)
                break;
            Vector3 playerPos = Manager.Game.Player.GetComponent<Transform>().position;
            // 갱신된 플레이어의 위치로 이동
            targetPos =  Vector3.MoveTowards(targetPos, playerPos, playerStat.MoveSpeed * 0.9f);
            // targetPos 위치로 원 이동
            yield return downTime += Time.deltaTime;
        }
        // 플레이어 위치로 내려찍음, 자세한 속도와 그런건 정해진게 없어서 그냥 하면 될듯?
        // 내려찍음
        // 3 단위미터 내 원형으로 데미지를 줌
        controller.ChangeState(BossState.Move);
        animator.SetBool(BOSS_PATTERN4 , false);
        yield return Manager.Instance.nowPattern = null;
    }
    public IEnumerator Pattern5()
    {
        Debug.Log("Pattern5 Invoke");
        animator.SetBool (BOSS_PATTERN5, true);
        // 3초에 걸려서 체력 100을 회복, 받는 데미지 30% 감소
        int healAmount = 100;
        float healTimer = 3f;
        float healTime = 0f;
        while (true)
        {
            if (healTime > healTimer)
                break;
            // 받는 데미지 30% 감소
            bossController.reduceDamageFlag = true;
            // 3초에 따른 호출당 피회복
            bossController.stat.Hp += healAmount/healTimer * Time.deltaTime;
            yield return healTime += Time.deltaTime;
        }
        bossController.reduceDamageFlag=false;
        controller.ChangeState(BossState.Move);
        animator.SetBool(BOSS_PATTERN5 , false);
        yield return Manager.Instance.nowPattern = null;
    }
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
