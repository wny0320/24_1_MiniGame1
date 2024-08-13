using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    public enum WeaponType
    {
        Nomal_Gun,M4,MiniGun,ShotGun,Sniper
    }

    [Header("총 유형")]
    public WeaponType weapontype;

    [Header("총 연사 속도")]
    public float fireRate;

    [Header("총알 갯수")]
    public int BulletCount;
    public int MaxBullet;

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("총알 스피드")]
    public float speed;

    [Header("재장전 갯수")]
    public int ReloadBullet;

    [Header("재장전 속도")]
    public float ReloadSpeed;

    [Header("이동속도 디버프")]
    public float SpeedDebuff;

    [Header("공격력")]
    public float Power;
}
