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

    // UI 관련 필드 추가
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

        // HP 슬라이더 초기화
        if (hpSlider != null)
        {
            hpSlider.maxValue = stat.MaxHp;
            hpSlider.value = stat.Hp;
        }

        // Finish 텍스트 초기화
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
        //상태 생성
        BaseState MoveState = new Boss1MoveState(this, rigid2D, animator, stat);
        BaseState PatternState = new Boss1PatternState(this, rigid2D, animator, stat);
        BaseState DieState = new DieState(this, rigid2D, animator);
        //상태 추가
        states.Add(BossState.Move, MoveState);
        states.Add(BossState.Pattern, PatternState);
        states.Add(BossState.Die, DieState);
        //state machine 초기값
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

        // HP 슬라이더 업데이트
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

        // Finish 텍스트 표시
        if (finishText != null)
        {
            finishText.alpha = 1;
        }

        // HP 슬라이더 비활성화
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(false); // 슬라이더 비활성화
        }

        // 게임 오브젝트 비활성화 및 게임 종료 로직 실행
        StartCoroutine(EndGame());
    }


    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        Debug.Log("Game Over - Boss Defeated");

        // 여기에 게임 종료 로직 추가 (예: 씬 전환, 게임 오버 화면 표시 등)
        // 예시: SceneManager.LoadScene("GameOverScene");

        // 보스 오브젝트 제거
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
