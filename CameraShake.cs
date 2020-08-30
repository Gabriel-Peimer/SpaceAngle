using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeStrength = 0.5f;
    public float duration = 0.6f;
    private float slowDownAmount = 1f;
    public static bool shouldShake;//so can be accessed from other scripts

    private Transform cameraTransform;
    //these are stored so that we can reset the camera after the shake
    private Vector3 initialPosition;
    private float initialDuration;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialPosition = cameraTransform.localPosition;
        initialDuration = duration;
    }

    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cameraTransform.localPosition += Random.insideUnitSphere * shakeStrength;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                //resetting values so that they can be used again
                shouldShake = false;
                duration = initialDuration;
                cameraTransform.localPosition = initialPosition;
            }
        }
    }
}
