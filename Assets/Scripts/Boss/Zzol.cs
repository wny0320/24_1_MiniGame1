using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zzol : MonoBehaviour
{
    private float speed = 20.0f;
    private Vector3 dir = Vector3.zero;
    public bool shootFlag = false;
    public GameObject zzolRangeObj = null;
    private void Update()
    {
        Shoot();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IReceiveAttack>()?.OnHit(40);
    }
    public void Shoot()
    {
        if (shootFlag == false)
            return;
        transform.position += dir * speed * Time.deltaTime;
        Vector3 zzolPos = Camera.main.WorldToViewportPoint(transform.position);
        if (zzolPos.x < 0 || zzolPos.x > 1 || zzolPos.y < 0 || zzolPos.y > 1)
            GameObject.Destroy(gameObject);
    }
    public void ShootSet(Vector3 _dir)
    {
        dir = _dir;
        shootFlag = true;
    }
}
