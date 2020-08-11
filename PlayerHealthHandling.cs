using UnityEngine;

public class PlayerHealthHandling : MonoBehaviour
{
    public float maxHealth = 2;
    public float currentHealth;

    public PlayerMovement playerMovement;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    /*
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (playerMovement.hasTouchEnded == false)
        {
            currentHealth -= 1f * Time.deltaTime;
        }
        else
        {
            currentHealth += 1f * Time.deltaTime;
        }
        healthBar.SetHealth(currentHealth);
    }*/
}
