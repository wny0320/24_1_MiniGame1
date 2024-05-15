using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Weapon weapon; // Weapon ��ũ��Ʈ�� �����ϱ� ���� ����
    public int currentBulletIndex = 0; // ���� �Ѿ� �ε���
    public Text bulletTypeText; // �Ѿ� ������ ǥ���� UI Text ���

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchBullet(); // Tab Ű�� ���� ������ �Ѿ� ����
        }
    }

    void SwitchBullet()
    {
        currentBulletIndex = (currentBulletIndex + 1) % weapon.bulletPrefabs.Length; // ���� �Ѿ� �ε��� ���
        weapon.ChangeBullet(currentBulletIndex); // ���� �Ѿ˷� ����
        UpdateUI(); // UI ������Ʈ
    }

    void UpdateUI()
    {
        // �Ѿ� ������ ǥ���ϴ� UI Text ������Ʈ
        if (bulletTypeText != null)
        {
            bulletTypeText.text = "Bullet Type: " + weapon.bulletPrefabs[currentBulletIndex].name;
        }
    }
}
