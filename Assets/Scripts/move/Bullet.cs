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
    private float bulletSpeed; // 총알 속도
    private static float currentBulletSpeed = 10f; // 현재 총알 속도 (초기값 10f)

    void Start()
    {
        bulletSpeed = currentBulletSpeed; // 시작 시 현재 총알 속도로 초기화
        gameObject.tag = "Bullet"; // 총알 오브젝트에 "Bullet" 태그 설정
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
        targetPos = _dir.normalized; // 방향을 정규화하여 설정
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Boss 스크립트를 찾음
        Boss boss = collision.GetComponent<Boss>();
        if (boss != null) // 보스에 닿았을 경우
        {
            boss.TakeDamage(damage); // 보스의 HP 감소
            Destroy(gameObject); // 총알 삭제
        }
    }
}
