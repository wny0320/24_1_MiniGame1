using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    [Header("�� ����")]
    public WeaponType weapontype;

    [Header("�� ���� �ӵ�")]
    public float fireRate;

    [Header("�Ѿ� ����")]
    public int BulletCount;
    public int MaxBullet;

    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab;

    [Header("�Ѿ� ���ǵ�")]
    public float speed;

    [Header("������ ����")]
    public int ReloadBullet;

    [Header("������ �ӵ�")]
    public float ReloadSpeed;

    [Header("�̵��ӵ� �����")]
    public float SpeedDebuff;

    [Header("���ݷ�")]
    public float Power;
}
