using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float movementForce;

    //for missile script
    private GameObject player;
    private Missile missileScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        missileScript = player.GetComponent<Missile>();
    }
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(0, 0, -movementForce * Time.fixedDeltaTime);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);//getting the touch

            var ray = Camera.main.ScreenPointToRay(touch.position);//creating a ray

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var obstacleTransform = hitInfo.collider.GetComponent<Transform>();
                if (hitInfo.collider.tag == "Obstacle")
                {
                    missileScript.targetTransform = obstacleTransform;
                    missileScript.targetObject = obstacleTransform.gameObject;
                }
            }
        }
    }
    private void OnMouseDown()
    {
        missileScript.targetTransform = gameObject.transform;
        missileScript.targetObject = gameObject;
    }
}
