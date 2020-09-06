using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private float length, startpos;
    public float parallaxEffect;
    private float distance;
    public float addToStartpos;

    void Start()
    {
        startpos = transform.position.z;
        length = GetComponent<MeshRenderer>().bounds.size.z;
        
        distance = 0;
    }
    void FixedUpdate()
    {
        distance += parallaxEffect;

        transform.position = new Vector3(transform.position.x, transform.position.y, startpos + addToStartpos - distance);
        
        if (transform.position.z <= -length)
        {
            distance = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, startpos + addToStartpos);
        }
    }
}
