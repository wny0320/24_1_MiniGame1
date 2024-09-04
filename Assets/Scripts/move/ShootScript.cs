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
    public AimController aimController; // 에임 위치 

    public Transform ShootPoint;

    [Header("현재 장착된 총")]
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

    void FaceMouse()   // 총이 마우스 포인터를 따라서 움직이도록
    {

        /* mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

         Vector3 rotation = mousePos - transform.position; ;

         float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

         transform.rotation = Quaternion.Euler(0, 0, rotZ);*/
        // 마우스 위치를 월드 좌표로 변환
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // 총의 위치에서 마우스 위치로의 회전 벡터를 계산
        Vector3 rotation = mousePos - transform.position;

        // 각도를 계산
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // 총의 회전 설정
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // 마우스가 총의 왼쪽에 있을 때 좌우 반전
        if (rotation.x < 0)
        {
            // 총을 좌우 반전시키기 위해 X축의 로컬 스케일을 -1로 설정
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            // 마우스가 총의 오른쪽에 있을 때는 원래대로 설정
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FireRateCalc() // 총 연사 속도를 위한 함수
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
            if (currentFireRate <= 0 && currentGun.BulletCount > 0)  // 연사속도에 맞게 총알이 있을때만 총알 발사
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
            currentGun.BulletCount--;                           // 총알 갯수 카운트

            // 에임 위치로 총알 발사
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
            Debug.Log("재장전");
        }
        
    }

}
