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
        bossStat.Hp = Mathf.Max(bossStat.Hp, 0); // 0 ���Ϸ� �������� �ʵ��� ��
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

        // ���� ������Ʈ�� ��� ��Ȱ��ȭ
        gameObject.SetActive(false);

        // ���� ���� ���� �߰�
        StartCoroutine(EndGame());
    }

    private System.Collections.IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f); // 2�� ���

        // ���⿡ ���� ���� ���� �߰� (��: �� ��ȯ, ���� ���� ȭ�� ǥ�� ��)
        Debug.Log("Game Over");

        // ���� ������Ʈ ������ ����
        Destroy(gameObject);
    }
}