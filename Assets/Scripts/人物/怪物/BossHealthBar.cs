using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image redBar;  // 红色血条
    [SerializeField]
    private Image whiteBar; // 白色延迟血条
    [SerializeField]
    private Text bossName; // Boss 名称
    [SerializeField]
    private float whiteBarFollowSpeed = 2f; // 白色血条跟随速度
    private float maxHealth;
    private float currentHealth;
    private float targetHealth;
    public void ReSetHealth(string name,float BOSSHealth)
    {
        maxHealth = BOSSHealth;
        bossName.text = name;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        targetHealth = health;
        UpdateHealthBar();
    }

    void Update()
    {
        // 白色血条缓慢跟随红色血条
        if (whiteBar.fillAmount > redBar.fillAmount)
        {
            whiteBar.fillAmount = Mathf.Lerp(whiteBar.fillAmount, redBar.fillAmount, Time.deltaTime * whiteBarFollowSpeed);
        }
    }

    private void UpdateHealthBar()
    {
        // 红色血条即时更新
        redBar.fillAmount = targetHealth / maxHealth;
    }
}
