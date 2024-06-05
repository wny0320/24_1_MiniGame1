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

      private void Update()
      {
          ChangeGun();
      }



    private void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (this.bullet.type == Bullet.eType.Gun1)
            {
                
                //무기교체
            }
            else if(this.bullet.type == Bullet.eType.Gun2)
            {
                //"무기교체"
            }  
        }
    }


}
