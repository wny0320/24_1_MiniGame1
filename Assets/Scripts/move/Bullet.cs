using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bullet : MonoBehaviour
{
    Vector3 targetPos = Vector3.zero;
    float damage = 1f;

    public enum eType
    {
        Gun1,Gun2
    }
    public eType type;
    private float bulletSpeed; // �Ѿ� �ӵ�
    private static float currentBulletSpeed = 10f; // ���� �Ѿ� �ӵ� (�ʱⰪ 10f)

    void Start()
    {
        bulletSpeed = currentBulletSpeed; // ���� �� ���� �Ѿ� �ӵ��� �ʱ�ȭ
    }

    void Update()
    {
       // ChangeGun();
        BulletMove();
    }

    private void BulletMove()
    {
        transform.position += targetPos * Time.deltaTime * bulletSpeed;
    }

    public void BulletDirSet(Vector3 _dir)
    {
        targetPos = _dir;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IReceiveAttack>() != null)
        {
            collision.GetComponent<IReceiveAttack>().OnHit(1f);
        }
    }

}