using UnityEngine;

public class PlayerHealthHandling : MonoBehaviour
{
    private float maxHealth = 2;
    public float currentHealth;

    public HealthBar healthBar;//for setting health
    public TimeManager timeManager;//for slowDownFactor variable
    private float[] slowMotionUpgrades = { 0, 1f, 1.5f, 2f, 2.5f };
    private GameMaster gameMaster;

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        //checking how much health the player should have according to upgrades
        maxHealth = slowMotionUpgrades[gameMaster.slowMotionUpgradeValue];
        
        if (gameMaster.slowMotionUpgradeValue > 0)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (gameMaster.slowMotionUpgradeValue > 0)
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
}
