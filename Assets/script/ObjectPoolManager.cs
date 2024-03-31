using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField]
    private GameObject objectPrefab; // 풀링할 오브젝트의 프리팹
    [SerializeField]
    private int poolSize = 20; // 초기 풀 사이즈

    private Queue<GameObject> objectPool = new Queue<GameObject>(); // 오브젝트 풀

    private void Awake()
    {
        Instance = this; // 싱글턴 인스턴스 할당
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // 오브젝트를 비활성화 상태로 생성
            objectPool.Enqueue(obj); // 오브젝트 풀에 추가
        }
    }

    // 오브젝트를 풀에서 꺼내어 사용
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue(); // 풀에서 오브젝트 꺼내기
            obj.SetActive(true); // 오브젝트 활성화
            return obj;
        }
        else
        {
            // 풀이 비어있으면 새로운 오브젝트 생성 (선택적)
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // 사용이 끝난 오브젝트를 풀로 반환
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트 비활성화
        objectPool.Enqueue(obj); // 오브젝트를 풀에 반환
    }
}
