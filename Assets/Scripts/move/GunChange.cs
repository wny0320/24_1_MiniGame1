using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    Bullet bullet;
    AimController aimcontroller;


    private void Start()
    {
        aimcontroller = GetComponent<AimController>();
        bullet = GetComponent<Bullet>();    
    }

  /*  private void Update()
    {
        Change();
    }*/



   /* private void Change()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet.SetBulletSpeed(100f);
            aimcontroller.Gun1();
            Debug.Log("asd");
        }
    }*/


}
