using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    private void Start()
    {
        Screen.SetResolution((int)Screen.width, (int)Screen.height, false);
    }
    void Update()
    {
        transform.position = player.position + offset;
    }
}
