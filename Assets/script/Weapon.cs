using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // ���� ������ �Ѿ� ������ �迭
    private GameObject currentBullet; // ���� Ȱ��ȭ�� �Ѿ�

    // �Ѿ� ����
    public void ChangeBullet(int bulletIndex)
    {
        if (bulletIndex >= 0 && bulletIndex < bulletPrefabs.Length)
        {
            // ���� �Ѿ��� Ȱ��ȭ�Ǿ� ������ ��Ȱ��ȭ�մϴ�.
            if (currentBullet != null)
            {
                currentBullet.SetActive(false);
            }

            // ������ �Ѿ��� Ȱ��ȭ�մϴ�.
            GameObject selectedBulletPrefab = bulletPrefabs[bulletIndex];
            selectedBulletPrefab.SetActive(true);

            // ���� �Ѿ��� �����մϴ�.
            currentBullet = selectedBulletPrefab;
        }
        else
        {
            Debug.LogError("Invalid bullet index!");
        }
    }
}
