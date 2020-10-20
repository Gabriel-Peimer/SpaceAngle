using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PlayerHealthHandling playerHealth;

    //slow motion
    public float slowDownFactor = 0.25f;
    public float slowDownLength = 2f;

    public bool shouldSlowMotionStop = false;//public so can be accessed from PlayerMovement script

    public PlayerMovement playerMovementScript;//so that we can speed up the player

    //to speed up the player in slow motion so that it feels like the player has more control
    private float startingSpeedComputer;
    private float startingSpeedMobile;

    private void Start()
    {
        startingSpeedComputer = playerMovementScript.sideForceComputer;
        startingSpeedMobile = playerMovementScript.sideForceMobile;
    }
    public void DoSlowmotion()
    {
        //speeding the player up
        if (playerMovementScript.sideForceComputer == startingSpeedComputer)
        {
            playerMovementScript.sideForceComputer *= 2f;
        }
        if (playerMovementScript.sideForceMobile == startingSpeedMobile)
        {
            playerMovementScript.sideForceMobile *= 2f;
        }
            

        shouldSlowMotionStop = false;

        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;   
    }
    void Update()
    {
        if (true)
        {
            shouldSlowMotionStop = true;
        }
        if (shouldSlowMotionStop)
        {
            //resetting the speed
            playerMovementScript.sideForceComputer = startingSpeedComputer;
            playerMovementScript.sideForceMobile = startingSpeedMobile;

            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
/*    public void StartSlowMotion()
    {
        DoSlowmotion();
    }
    public void StopSlowMotion()
    {
        shouldSlowMotionStop = true;
    }*/
}
