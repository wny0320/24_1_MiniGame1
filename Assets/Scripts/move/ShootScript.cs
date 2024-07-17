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
        /*Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;*/
        FaceMouse();
        FireRateCalc();
        TryFire();

    }

    void FaceMouse()
    {
        /*Gun.transform.right = direction;*/
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position; ;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void FireRateCalc()
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
            if (currentFireRate <= 0 && currentGun.BulletCount > 0)
            {
                currentFireRate = currentGun.fireRate;
                Shoot();
            }
        }

    }

    void Shoot()
    {
        Vector3 inputPos;
        if (Input.GetMouseButtonDown(0))
        {
            currentGun.BulletCount--;
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z; // ī�޶�� ���콺�� �Ÿ��� ����Ͽ� z ��ǥ ����
            inputPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            GameObject nowBullet = Manager.Bullet.GetBullet();
            inputPos = Vector3.Normalize(inputPos - transform.position); // ����� ��ġ�� ����ȭ
            Manager.Bullet.BulletInit(nowBullet, transform.position, inputPos);
        }

    }

}
