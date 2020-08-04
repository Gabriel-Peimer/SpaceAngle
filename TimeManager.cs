using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PlayerHealthHandling playerHealth;
    public PlayerMovement playerMovement;

    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;
    //public float timeLeftForSlowDown;

    public void DoSlowmotion()
    {
        if (playerHealth.currentHealth > 0)
        {
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //timeLeftForSlowDown = slowDownLength;
        }
    }
    void Update()
    {
        if (playerHealth.currentHealth > 0.05f && playerMovement.hasTouchEnded == true)
        {
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            //timeLeftForSlowDown -= 1f * Time.deltaTime;
            //timeLeftForSlowDown = Mathf.Clamp(timeLeftForSlowDown, 0f, 2f);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
        else if (playerHealth.currentHealth <= 0.05f)
        {
            Time.timeScale += (2f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }
    
}
