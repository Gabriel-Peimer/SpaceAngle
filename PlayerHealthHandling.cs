using UnityEngine;

public class PlayerHealthHandling : MonoBehaviour
{
    private float maxHealth = 2;
    public float currentHealth;

    public HealthBar healthBar;//for setting health
    public TimeManager timeManager;//for slowDownFactor variable

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        if (timeManager != null)
        {
            if (Time.timeScale <= timeManager.slowDownFactor)//time to take health away
            {
                currentHealth -= 1f * Time.unscaledDeltaTime;
            }
            else if (Time.timeScale > timeManager.slowDownFactor)//time to recharge the health bar
            {
                currentHealth += 0.5f * Time.unscaledDeltaTime;
            }
            healthBar.SetHealth(currentHealth);
        }
    }
}
