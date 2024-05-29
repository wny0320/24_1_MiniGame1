using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;

using UnityEngine;

using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 targetPos = Vector3.zero;
    private float bulletSpeed; // �Ѿ� �ӵ�
    private static float currentBulletSpeed = 10f; // ���� �Ѿ� �ӵ� (�ʱⰪ 10f)

    void Start()
    {
        bulletSpeed = currentBulletSpeed; // ���� �� ���� �Ѿ� �ӵ��� �ʱ�ȭ
    }

    void Update()
    {
        ChangeGun();
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

    private void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletSpeed(); // �Ѿ� �ӵ� ��ȯ
        }
    }

    private void BulletSpeed()
    {
        currentBulletSpeed = currentBulletSpeed == 10f ? 100f : 10f; // 10f�� 100f ���̿��� ��ȯ
    }
}