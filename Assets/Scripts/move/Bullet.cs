using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 targetPos = Vector3.zero;
    float damage = 1f;

    public enum eType
    {
        Gun1,
        Gun2
    }
    public eType type;
    private float bulletSpeed; // �Ѿ� �ӵ�
    private static float currentBulletSpeed = 10f; // ���� �Ѿ� �ӵ� (�ʱⰪ 10f)

    void Start()
    {
        bulletSpeed = currentBulletSpeed; // ���� �� ���� �Ѿ� �ӵ��� �ʱ�ȭ
        gameObject.tag = "Bullet"; // �Ѿ� ������Ʈ�� "Bullet" �±� ����
    }

    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        transform.position += targetPos * Time.deltaTime * bulletSpeed;
    }

    public void BulletDirSet(Vector3 _dir)
    {
        targetPos = _dir.normalized; // ������ ����ȭ�Ͽ� ����
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Boss ��ũ��Ʈ�� ã��
        Boss boss = collision.GetComponent<Boss>();
        if (boss != null) // ������ ����� ���
        {
            boss.TakeDamage(damage); // ������ HP ����
            Destroy(gameObject); // �Ѿ� ����
        }
    }
}
