using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    // ���� ���� �ڷ�ƾ
    private Coroutine patternCoroutine;
    // �ܺο��� �־���ϴ� ��
    public NowBoss bossNum = NowBoss.Null;
    // ���� ������ ����
    private int bossPatternCount;
    // �÷��̾��� Transform
    public Transform playerTrans;
    // ������ ���۵Ǵ� �ð�
    public float patternTime = 5f;
    // �����ġ�� ���� Ÿ�̸�, �ʱⰪ 0
    public float patternTimer = 0f;
    // ���� �Ÿ�
    public float unitDis = 0.1f;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        stat = GetComponent<Stat>();
        animator = GetComponent<Animator>();
        InitStateMachine();
    }

    void Update()
    {
        stateMachine?.StateUpdateFunc();
        GetPlayerTrans();
    }

    private void FixedUpdate()
    {
        stateMachine?.StateFixtedUpdateFunc();
    }

    private void InitStateMachine()
    {
        //���� ����
        BaseState MoveState = new BossMoveState(this, rigid2D, animator, stat);
        //BaseState PatternState = new BossPatternState(this, rigid2D, animator, stat);

        //���� �߰�
        states.Add(BossState.Move, MoveState);
        //states.Add(BossState.Pattern, PatternState);

        //state machine �ʱⰪ
        stateMachine = new StateMachine(MoveState);
    }
    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
        if((BossState)s == BossState.Pattern)
        {

        }
    }
    public void GetPlayerTrans()
    {
        if (playerTrans == null)
            playerTrans = Manager.Game.Player.transform;
        else
            return;
    }

    public override void OnHit(float damage)
    {

    }
    //private void PatternSelect()
    //{
    //    // �÷��̾��� ��ġ�� �޾ƿ��� ���ߴٸ� return
    //    if (playerTrans == null)
    //        return;
    //    // ������ �������� �ʾҴٸ� return
    //    if (bossNum == BossNum.Null)
    //        return;
    //    float playerDis = Vector3.Magnitude((Vector2)(playerTrans.position) - rigid2D.position);
    //    // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
    //    #region ���� ���� ���ǹ�
    //    bool[] patternTrigger = new bool[bossPatternCount];
    //    List<int> activeTriggerList = new List<int>();
    //    if (bossNum == BossNum.Boss1)
    //    {
    //        bossPatternCount = Enum.GetValues(typeof(Boss1Pattern)).Length;
    //        patternTrigger = new bool[bossPatternCount];

    //        //���� 1 ���� ����
    //        patternTrigger[0] = false;
    //        if (playerDis < unitDis * 1.3f)
    //            patternTrigger[1] = true;
    //        if (playerDis < unitDis * 8f)
    //            patternTrigger[2] = true;
    //        if (playerDis > unitDis * 12f)
    //            patternTrigger[3] = true;
    //        if (playerDis < unitDis * 20f)
    //            patternTrigger[4] = true;

    //        for (int i = 0; i < bossPatternCount; i++)
    //        {
    //            if (patternTrigger[i] == true)
    //                activeTriggerList.Add(i);
    //        }

    //        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
    //        int selectedIndex = activeTriggerList[n];
    //        string patternName = "Pattern" + selectedIndex;
    //        Boss1Pattern targetPattern = (Boss1Pattern)Enum.Parse(typeof(Boss1Pattern), patternName);
    //        //���� �������� �ʾ����Ƿ� Move���·� ������
    //        //controller.ChangeState(targetPattern);
    //        ChangeState(BossState.Move);
    //    }
    //    if (bossNum == BossNum.Boss2)
    //    {
    //        //bossPatternCount = Enum.GetValues(typeof(Boss2Pattern)).Length;
    //        //patternTrigger = new bool[bossPatternCount];
    //    }
    //    #endregion
    //}
    private IEnumerator Boss1Pattern1()
    {
        yield return null;
    }
}
