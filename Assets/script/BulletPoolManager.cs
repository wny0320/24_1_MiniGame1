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
            // �θ� �������ִ� �ڵ�, �� �ڵ尡 ������ ���̶�Űâ�� �ʹ� ����������
            newBullet.transform.parent = transform;
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
            // Ǯ�� ������� ���, ���ο� �Ѿ� ����
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
