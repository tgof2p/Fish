using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First : MonoBehaviour 
{
    float speed = 10f;
    private Vector3 previousPos;
    public Camera cam;
    Vector3 viewPos;
    Vector3 targetPosition;
    Vector3 newPosition;

    //private Vector3 offset;
    private const float MIN_VIEW_POS_X = 0.01f;
    private const float MAX_VIEW_POS_X = 0.99f;
    private const float MIN_VIEW_POS_Y = 0.01f;
    private const float MAX_VIEW_POS_Y = 0.99f;


	// Use this for initialization
	void Start () 
    {
        Debug.Log("Start!");
        previousPos = transform.position;
        targetPosition = transform.position;
        newPosition = transform.position;
    
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        //Debug.Log(viewPos);
        //Movement();	
        LookDirection();
        mousePosition();
	}

    void Movement()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (dx != 0 || dy != 0)
        {
            Vector3 newPosition = transform.position + new Vector3(dx, dy, 0);
            viewPos = cam.WorldToViewportPoint(newPosition);

            if (viewPos.x > MIN_VIEW_POS_X && viewPos.x < MAX_VIEW_POS_X
           && viewPos.y > MIN_VIEW_POS_Y && viewPos.y < MAX_VIEW_POS_Y)
            {
                transform.position = newPosition;
            }
        }
    }

    void LookDirection()
    {
        Vector3 diff = previousPos - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if (rot_z >= -90 && rot_z <= 90)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 180 - rot_z);
        }
        previousPos = transform.position;
    }

    void mousePosition()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            Debug.Log(targetPosition);
        }

        viewPos = cam.WorldToViewportPoint(targetPosition);



        if (viewPos.x > MIN_VIEW_POS_X && viewPos.x < MAX_VIEW_POS_X
       && viewPos.y > MIN_VIEW_POS_Y && viewPos.y < MAX_VIEW_POS_Y)
        {
            newPosition = targetPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
    
    }
}
