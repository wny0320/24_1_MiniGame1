using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    //public static BulletPoolManager Instance;
    const string PREFAB_PATH = "prefabs/bullet";
    const string BULLET_POOL = "BulletPool";

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int initialPoolSize = 100;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    public void OnAwake()
    {
        //Instance = this;
        GetBulletPrefab();
        InitializeBulletPool();
    }
    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            // 부모를 지정해주는 코드, 이 코드가 없으면 하이라키창이 너무 지저분해짐
            newBullet.transform.parent = GameObject.Find(BULLET_POOL).transform;
            newBullet.SetActive(false);
            newBullet.tag = "Bullet";
            bulletPool.Enqueue(newBullet);
        }
    }

    public void GetBulletPrefab()
    {
        if (bulletPrefab == null)
            bulletPrefab = Resources.Load(PREFAB_PATH, typeof(GameObject)) as GameObject;
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
            newBullet.tag = "Bullet";
            return newBullet;
        }
    }
    public void BulletInit(GameObject _bullet, Vector3 _initPos, Vector3 _dir)
    {
        _bullet.transform.position = _initPos;
        _bullet.TryGetComponent<Bullet>(out Bullet bulletComp);
        bulletComp?.BulletDirSet(_dir);
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
