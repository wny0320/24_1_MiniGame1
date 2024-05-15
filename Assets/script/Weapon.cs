using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // 여러 종류의 총알 프리팹 배열
    private GameObject currentBullet; // 현재 활성화된 총알

    // 총알 변경
    public void ChangeBullet(int bulletIndex)
    {
        if (bulletIndex >= 0 && bulletIndex < bulletPrefabs.Length)
        {
            // 이전 총알이 활성화되어 있으면 비활성화합니다.
            if (currentBullet != null)
            {
                currentBullet.SetActive(false);
            }

            // 선택한 총알을 활성화합니다.
            GameObject selectedBulletPrefab = bulletPrefabs[bulletIndex];
            selectedBulletPrefab.SetActive(true);

            // 현재 총알을 갱신합니다.
            currentBullet = selectedBulletPrefab;
        }
        else
        {
            Debug.LogError("Invalid bullet index!");
        }
    }
}
