using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimchange : MonoBehaviour
{

    int totalWeapons = 1;
    public int currentWeaponindex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun; 

    
    void Start()
    {
        GunSetting();
    }

   
   
    
    void Update()
    {
        GunChange();
    }

   
    
    
    public void GunChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeaponindex < totalWeapons - 1)
            {
                guns[currentWeaponindex].SetActive(false);
                currentWeaponindex++;
                guns[currentWeaponindex].SetActive(true);
                currentGun = guns[currentWeaponindex];
            }
            else
            {
                guns[currentWeaponindex].SetActive(false);
                currentWeaponindex = 0;
                guns[currentWeaponindex].SetActive(true);
                currentGun = guns[currentWeaponindex];
            }
        }
    }

   
    
    
    public void GunSetting()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponindex = 0;
    }
}
