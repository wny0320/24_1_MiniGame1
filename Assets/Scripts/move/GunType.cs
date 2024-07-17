using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    public enum WeaponType
    {
        Nomal_Gun,Gun2
    }

    [Header("ÃÑ À¯Çü")]
    public WeaponType weapontype;

    [Header("ÃÑ ¿¬»ç ¼Óµµ")]
    public float fireRate;

    [Header("ÃÑ¾Ë °¹¼ö")]
    public int BulletCount;
    public int MaxBullet;

    [Header("ÃÑ¾Ë ÇÁ¸®ÆÕ")]
    public GameObject BulletPrefab;

    [Header("ÃÑ¾Ë ½ºÇÇµå")]
    public float speed;
}
