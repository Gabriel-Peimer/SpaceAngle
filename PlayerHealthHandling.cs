using UnityEngine;

public class PlayerHealthHandling : MonoBehaviour
{
    public float maxHealth = 2;
    public float currentHealth;

    public HealthBar healthBar;//for setting health
    public TimeManager timeManager;//for slowDownFactor variable
    private float[] slowMotionUpgrades = { 2f, 2.75f, 3.75f, 4.5f, 5f };
    private GameMaster gameMaster;

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        //checking how much health the player should have according to upgrades
        maxHealth = slowMotionUpgrades[gameMaster.slowMotionUpgradeValue];

        if (GameManager.gameHasEnded == false)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            healthBar.gameObject.SetActive(true);
        }
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
            healthBar.SetHealth(currentHealth);
        }
    }
}
