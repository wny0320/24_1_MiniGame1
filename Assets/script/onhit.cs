using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public Image[] healthBars;
    private int currentHealthIndex;

    void Start()
    {
        currentHealthIndex = healthBars.Length - 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bullet과 충돌할 때
        if (collision.gameObject.tag == "Bullet")
        {
            DecreaseHealthBarOpacity();
        }
    }

    void DecreaseHealthBarOpacity()
    {
        if (currentHealthIndex >= 0)
        {
            Color color = healthBars[currentHealthIndex].color;
            color.a = Mathf.Max(color.a - 0.2f, 0); 
            healthBars[currentHealthIndex].color = color;

            currentHealthIndex--;
        }
    }
}
