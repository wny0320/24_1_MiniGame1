using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int initialPoolSize = 100;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializeBulletPool();
    }

    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            bulletPool.Enqueue(newBullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // 풀이 비어있을 경우, 새로운 총알 생성
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(true);
            return newBullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
