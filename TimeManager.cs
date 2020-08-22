using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PlayerHealthHandling playerHealth;

    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;
    public bool shouldSlowMmotionStop = false;//public so can be accessed from PlayerMovement script

    public void DoSlowmotion()
    {
        if (playerHealth.currentHealth > 0)
        {
            shouldSlowMmotionStop = false;

            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            shouldSlowMmotionStop = true;
        }
        if (shouldSlowMmotionStop)
        {
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}
