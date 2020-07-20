using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundMovement : MonoBehaviour
{
    private float length, startpos;
    public float parallaxEffect;
    private float distance;
    public float addToStartpos;
    //private float dupeDistance;
    //public GameObject backgroundDupe;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.z;
        length = GetComponent<MeshRenderer>().bounds.size.z;
        
        //backgroundDupe.transform.position = new Vector3(transform.position.x, transform.position.y, startpos + length * 2);
        
        distance = 0;
        //dupeDistance = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance += parallaxEffect;
        //dupeDistance += parallaxEffect;

        transform.position = new Vector3(transform.position.x, transform.position.y, startpos + addToStartpos - distance);
        
        if (transform.position.z <= -length)
        {
            distance = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, startpos + addToStartpos);
        }/*
        if (dupeDistance >= length * 1.5)
        {
            dupeDistance = 0;
            backgroundDupe.transform.position = new Vector3(transform.position.x, transform.position.y, startpos + length);
        }
        if (distance >= 20)
        {
            backgroundDupe.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + length);
        }else if (distance < 20)
        {
            backgroundDupe.transform.position = new Vector3(transform.position.x, transform.position.y, startpos + length - dupeDistance);
        }*/
    }
}
