using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bullet : MonoBehaviour
{
    Vector3 targetPos = Vector3.zero;
    public enum eType
    {
        Gun1,Gun2
    }
    public eType type;
    private float bulletSpeed; // 총알 속도
    private static float currentBulletSpeed = 10f; // 현재 총알 속도 (초기값 10f)

    void Start()
    {
        bulletSpeed = currentBulletSpeed; // 시작 시 현재 총알 속도로 초기화
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

   

    
}