using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Weapon weapon; // Weapon 스크립트에 접근하기 위한 변수
    public int currentBulletIndex = 0; // 현재 총알 인덱스
    public Text bulletTypeText; // 총알 종류를 표시할 UI Text 요소

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchBullet(); // Tab 키를 누를 때마다 총알 변경
        }
    }

    void SwitchBullet()
    {
        currentBulletIndex = (currentBulletIndex + 1) % weapon.bulletPrefabs.Length; // 다음 총알 인덱스 계산
        weapon.ChangeBullet(currentBulletIndex); // 다음 총알로 변경
        UpdateUI(); // UI 업데이트
    }

    void UpdateUI()
    {
        // 총알 종류를 표시하는 UI Text 업데이트
        if (bulletTypeText != null)
        {
            bulletTypeText.text = "Bullet Type: " + weapon.bulletPrefabs[currentBulletIndex].name;
        }
    }
}
