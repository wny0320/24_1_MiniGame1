using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
public class BossPatternState : BaseState
{
    Transform playerTrans;
    // �÷��̾���� �Ÿ�
    float playerDis;
    // �÷��̾� ���� ���� ����
    Vector3 playerDir;
    // �ӽ� ��������
    float unitDistance = 1f;
    // ������ ����
    int patternCount = 6;
    // ���� �Լ��� ����ƴ��� �ľ��� flag
    bool funcFlag = false;
    float moveSpeed = 1f;
    public BossPatternState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
        : base(controller, rb, animator)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        GetPlayerTrans();
        PatternSelect();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
    private void GetPlayerTrans()
    {
        if (playerDir == null || playerTrans == null)
        {
            playerTrans = Manager.Game.Player.transform;
            playerDir = Vector3.Normalize((Vector2)playerTrans.position - rb.position);
            playerDis = Vector3.Magnitude((Vector2)playerTrans.position - rb.position);
        }
    }
    private async void PatternSelect()
    {
        if (playerDir == null || playerTrans == null)
            return;
        if (funcFlag == true)
            return;
        // ���ϸ��� ������ �����Ǿ����� ��� �ִ� Ʈ���� ���
        bool[] patternTrigger = new bool[patternCount];
        #region ���� ���� ���ǹ�
        if(playerDis > unitDistance * 15f)
            patternTrigger[0] = true;
        if(playerDis < unitDistance * 1.3f)
            patternTrigger[1] = true;
        if(playerDis < unitDistance * 8f)
            patternTrigger[2] = true;
        if (playerDis > unitDistance * 12f)
            patternTrigger[3] = true;
        if (playerDis < unitDistance * 20f)
            patternTrigger[4] = true;
        //ü�� ���� ���� ����
        #endregion
        #region ���� ���� Invoke
        List<int> activeTriggerList = new List<int>();
        for(int i = 0; i < patternCount; i++)
        {
            if (patternTrigger[i] == true)
                activeTriggerList.Add(i);
        }
        int n = UnityEngine.Random.Range(0, activeTriggerList.Count);
        int selectedIndex = activeTriggerList[n];
        string funcName = "Pattern" + selectedIndex;
        System.Reflection.MethodInfo patternMethod = GetType().GetMethod(funcName);
        funcFlag = true;
        //patternMethod?.Invoke(this, null);
        await new Task(() =>
        {
            Pattern2();
        });
        #endregion
    }
    private void InitPlayerData()
    {
        playerTrans = null;
        playerDis = float.NaN;
        playerDir = Vector3.zero;
        funcFlag = false;
    }
    #region ���� �Լ���
    public async void Pattern0()
    {
        Debug.Log("Pattern0 Invoked");
        float timer = 0f;
        float time = 2f;

        await new Task(() =>
        {
            while(timer < time)
            {
                timer += Time.deltaTime;
                rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 2.5f;
            }
        });
        //���� ������ �ϰ� ������ �ʱ�ȭ
        InitPlayerData();
    }
    public async void Pattern1()
    {
        Debug.Log("Pattern1 Invoked");
        float time = 0.8f;
        int number = 5;
        await new Task (() =>
        {
            new WaitForSeconds(1f);
        });
        await new Task(() =>
        {
            for(int i = 0; i < number; i++)
            {
                new WaitForSeconds(time);
                //��� ���
            }
        });
        //������Ʈ �ٲٱ�
        controller.ChangeState(BossState.Move);
    }
    public async void Pattern2()
    {
        Debug.Log("Pattern2 Invoked");
        //await new Task(() =>
        //{
        //    new WaitForSeconds(1.3f);
        //});
        //float timer = 0f;
        //float time = 1f;
        ////��� ���
        //await new Task(() =>
        //{
        //    while (timer < time)
        //    {
        //        timer += Time.deltaTime;
        //        rb.transform.position += playerDir * Time.deltaTime * moveSpeed * 4f;
        //    }
        //});
        ////�ĵ�����
        //await new Task(() =>
        //{
        //    new WaitForSeconds(3f);
        //});
        //������Ʈ �ٲٱ�
        controller.ChangeState(BossState.Move);
    }
    public async void Pattern3()
    {
        Debug.Log("Pattern3 Invoked");
        float timer = 0f;
        float time = 4f;
        // ���� ��ȯ
        await new Task(() =>
        {
            Vector3 realPlayerPos = playerTrans.position;
            new WaitForSeconds(0.8f);
            while(timer < time)
            {
                //���� ����
            }
            //ȭ�� �߻�
        });
        //������Ʈ �ٲٱ�
        controller.ChangeState(BossState.Move);
    }
    public async void Pattern4()
    {
        Debug.Log("Pattern4 Invoked");
        await new Task(() =>
        {
            //���� Į ���
            new WaitForSeconds(1.5f);
            float timer = 0f;
            float time = 1f;
            //����
            while(timer < time)
            {
                rb.transform.position += Vector3.up * Time.deltaTime;
            }
            //�������
            while (timer < time)
            {
                rb.transform.position += Vector3.down * Time.deltaTime;
            }
        });
        controller.ChangeState(BossState.Move);
    }
    public async void Pattern5()
    {
        Debug.Log("Pattern5 Invoked");
        await new Task(() =>
        {
            // 100ȸ���Ҷ����� ȸ��
            // 30% ����
        });
    }
    #endregion
}
