using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : BaseController, IReceiveAttack
{
    public NowBoss nowBoss;
    public bool reduceDamageFlag = false;
    public float noDamagedTime = 0f;
    public bool phase2Flag = false;
    public float timeAdd = 0f;
    public bool timeAddFlag = false;
    public float phase2TimeMul = 1.2f;
    public float phase2DamageMul = 1.3f;
    public float patternTime = 5f;
    public float patternTimer = 0f;
    public float unitDis = 1f;
    public const string X_DIR = "xDir";
    public const string Y_DIR = "yDir";
    public GameObject zzol;
    public GameObject pattern4Range;

    // UI ���� �ʵ� �߰�
    public Slider hpSlider;
    public CanvasGroup finishText;

    private bool isDead = false;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        stat = GetComponent<Stat>();
        animator = GetComponent<Animator>();
        InitStateMachine();

        // HP �����̴� �ʱ�ȭ
        if (hpSlider != null)
        {
            hpSlider.maxValue = stat.MaxHp;
            hpSlider.value = stat.Hp;
        }

        // Finish �ؽ�Ʈ �ʱ�ȭ
        if (finishText != null)
        {
            finishText.alpha = 0;
        }
    }

    void Update()
    {
        if (!isDead)
        {
            stateMachine?.StateUpdateFunc();
            SetAnimator();
            noDamagedTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            stateMachine?.StateFixtedUpdateFunc();
        }
    }

    private void InitStateMachine()
    {
        //���� ����
        BaseState MoveState = new Boss1MoveState(this, rigid2D, animator, stat);
        BaseState PatternState = new Boss1PatternState(this, rigid2D, animator, stat);
        BaseState DieState = new DieState(this, rigid2D, animator);
        //���� �߰�
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);
        states.Add(BossState.Die, DieState);
        //state machine �ʱⰪ
        stateMachine = new StateMachine(MoveState);
    }

    public override void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(BossState)s]);
    }

    public void OnHit(float damage)
    {
        if (isDead) return;

        noDamagedTime = 0f;
        if (reduceDamageFlag == true)
        {
            damage *= 0.7f;
        }
        stat.Hp -= damage;

        // HP �����̴� ������Ʈ
        if (hpSlider != null)
        {
            hpSlider.value = stat.Hp;
        }

        if (stat.Hp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        isAlive = false;
        ChangeState(BossState.Die);

        // Finish �ؽ�Ʈ ǥ��
        if (finishText != null)
        {
            finishText.alpha = 1;
        }

        // HP �����̴� ��Ȱ��ȭ
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(false); // �����̴� ��Ȱ��ȭ
        }

        // ���� ������Ʈ ��Ȱ��ȭ �� ���� ���� ���� ����
        StartCoroutine(EndGame());
    }


    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f); // 2�� ���

        Debug.Log("Game Over - Boss Defeated");

        // ���⿡ ���� ���� ���� �߰� (��: �� ��ȯ, ���� ���� ȭ�� ǥ�� ��)
        // ����: SceneManager.LoadScene("GameOverScene");

        // ���� ������Ʈ ����
        Destroy(gameObject);
    }

    public void PatternTimeAdder(float _time)
    {
        timeAdd = _time;
        timeAddFlag = true;
    }

    public void SetAnimator()
    {
        Transform playerTrans = Manager.Game.Player.transform;
        Vector3 playerDir = Vector3.Normalize((Vector2)playerTrans.position - rigid2D.position);
        animator.SetFloat(X_DIR, playerDir.x);
        animator.SetFloat(Y_DIR, playerDir.y);
    }
}
