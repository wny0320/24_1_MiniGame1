using UnityEngine;
using UnityEngine.UI;
using receive;

public class Boss : MonoBehaviour, IReceiveAttack
{
    public Slider hpSlider;
    private Stat bossStat;
    private HPUIManager hpUIManager;
    public CanvasGroup finishText;
    private bool isDead = false;

    void Start()
    {
        bossStat = GetComponent<Stat>();
        hpUIManager = GetComponent<HPUIManager>();
        hpSlider.maxValue = bossStat.MaxHp;
        hpSlider.value = bossStat.Hp;
        hpUIManager.UpdateHPUI(bossStat.Hp, bossStat.MaxHp);
        finishText.alpha = 0;
    }

    public void OnHit(float damage)
    {
        if (!isDead)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        bossStat.Hp -= damage;
        bossStat.Hp = Mathf.Max(bossStat.Hp, 0); // 0 이하로 내려가지 않도록 함
        hpSlider.value = bossStat.Hp;
        hpUIManager.UpdateHPUI(bossStat.Hp, bossStat.MaxHp);
        Debug.Log($"Boss HP: {bossStat.Hp}");

        if (bossStat.Hp <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Boss Died");
        finishText.alpha = 1;

        // 게임 오브젝트를 즉시 비활성화
        gameObject.SetActive(false);

        // 게임 종료 로직 추가
        StartCoroutine(EndGame());
    }

    private System.Collections.IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        // 여기에 게임 종료 로직 추가 (예: 씬 전환, 게임 오버 화면 표시 등)
        Debug.Log("Game Over");

        // 게임 오브젝트 완전히 제거
        Destroy(gameObject);
    }
}