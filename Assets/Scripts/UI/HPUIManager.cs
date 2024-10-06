using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    public Image hp5Image; // HP5_F 이미지
    public Image hp4Image; // HP4_F 이미지
    public Image hp3Image; // HP3_F 이미지
    public Image hp2Image; // HP2_F 이미지

    public void UpdateHPUI(float currentHp, float maxHp)
    {
        float ratio = currentHp / maxHp;

        // HP에 따라 투명도 조절
        hp5Image.color = new Color(hp5Image.color.r, hp5Image.color.g, hp5Image.color.b, ratio >= 1f ? 1f : 0f);
        hp4Image.color = new Color(hp4Image.color.r, hp4Image.color.g, hp4Image.color.b, ratio >= 0.8f ? 1f : 0f);
        hp3Image.color = new Color(hp3Image.color.r, hp3Image.color.g, hp3Image.color.b, ratio >= 0.6f ? 1f : 0f);
        hp2Image.color = new Color(hp2Image.color.r, hp2Image.color.g, hp2Image.color.b, ratio >= 0.4f ? 1f : 0f);
    }
}
