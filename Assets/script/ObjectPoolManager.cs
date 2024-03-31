using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField]
    private GameObject objectPrefab; // Ǯ���� ������Ʈ�� ������
    [SerializeField]
    private int poolSize = 20; // �ʱ� Ǯ ������

    private Queue<GameObject> objectPool = new Queue<GameObject>(); // ������Ʈ Ǯ

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� �Ҵ�
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ ���·� ����
            objectPool.Enqueue(obj); // ������Ʈ Ǯ�� �߰�
        }
    }

    // ������Ʈ�� Ǯ���� ������ ���
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue(); // Ǯ���� ������Ʈ ������
            obj.SetActive(true); // ������Ʈ Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ��������� ���ο� ������Ʈ ���� (������)
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        objectPool.Enqueue(obj); // ������Ʈ�� Ǯ�� ��ȯ
    }
}
