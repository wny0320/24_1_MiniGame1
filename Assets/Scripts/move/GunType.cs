using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    public enum WeaponType
    {
        Nomal_Gun,Gun2
    }

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
}
