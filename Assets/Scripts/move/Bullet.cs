using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    Vector3 targetPos = Vector3.zero;
    float bulletSpeed = 7f;

    // Update is called once per frame
    void Update()
    {
        //BulletDestroy();
        BulletMove();
    }


    //void BulletDestroy()
    //{
    //    if (transform.position.y > 9 || transform.position.x > 9)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void BulletMove()
    {
        transform.position += targetPos * bulletSpeed * Time.deltaTime;
    }
    public void BulletDirSet(Vector3 _dir)
    {
        targetPos = _dir;
    }
}
