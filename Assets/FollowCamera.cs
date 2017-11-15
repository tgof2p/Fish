using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour 
{


    public Transform objectToFollow;
    public Camera cam;
    private Vector3 offset;
    private const float MIN_VIEW_POS = 0.01f;
    private const float MAX_VIEW_POS = 0.99f;
    // Use this for initialization

    void Start()
    {
        Debug.Log("Start camera");
        offset = transform.position - objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(objectToFollow.position);

      
        if (viewPos.x < MIN_VIEW_POS || viewPos.x > MAX_VIEW_POS
            || viewPos.y < MIN_VIEW_POS || viewPos.y > MAX_VIEW_POS)
        {
            cam.transform.position = objectToFollow.transform.position + offset;
        }

    }

}
