using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;


public class ShootScript : MonoBehaviour
{
    public Transform Gun;

    Vector2 direction;

    public GameObject Bullet;

    [SerializeField]
    public AimController aimController; // ���� ��ġ 

    public Transform ShootPoint;

    [Header("���� ������ ��")]
    [SerializeField] GunType currentGun;

    float currentFireRate;

    private Camera mainCam;
    private Vector3 mousePos;


    void Start()
    {
        currentFireRate = 0;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        FaceMouse();
        FireRateCalc();
        TryFire();

    }

    void FaceMouse()   // ���� ���콺 �����͸� ���� �����̵���
    {
       
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position; ;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void FireRateCalc() // �� ���� �ӵ��� ���� �Լ�
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    void TryFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentFireRate <= 0 && currentGun.BulletCount > 0)  // ����ӵ��� �°� �Ѿ��� �������� �Ѿ� �߻�
            {
                currentFireRate = currentGun.fireRate;
                Shoot();
            }
            else if(currentGun.BulletCount <= 0)
            {
                Invoke("Reload",currentGun.ReloadSpeed);
            }
            
        }

    }

    void Shoot()
    {
        //Vector3 inputPos;
        if (Input.GetMouseButtonDown(0))
        {
            currentGun.BulletCount--;                           // �Ѿ� ���� ī��Ʈ

            // ���� ��ġ�� �Ѿ� �߻�
            Vector3 aimPosition = aimController.AimObject.position;
            Vector3 shootDirection = Vector3.Normalize(aimPosition - transform.position);

            GameObject nowBullet = Manager.Bullet.GetBullet();
            Manager.Bullet.BulletInit(nowBullet, transform.position, shootDirection);
        }

    }
    void Reload()
    {
        if (currentGun.BulletCount <= 0)
        {
            currentGun.BulletCount += currentGun.ReloadBullet;
            Debug.Log("������");
        }
        
    }

}
