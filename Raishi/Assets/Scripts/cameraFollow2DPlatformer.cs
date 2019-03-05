using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraFollow2DPlatformer : MonoBehaviour {

    public Transform target; //what the camera is following
    public float smoothing; //dampening effect that occurs on the camera

    Vector3 offset; //difference between the character and the camera
    Vector3 targetCamPos; //where the camera wants to be

    float lowY; //the lowest point that our camera can go
    float leftX; //the furthest left point that our camera can go

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            lowY = transform.position.y;
            leftX = transform.position.x;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            lowY = -3f;
            leftX = transform.position.x;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            lowY = -13f;
            leftX = -3f;
        }

    }

    void FixedUpdate()
    {
        if (target != null)
        {
            targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            //lerp allows us to move from one position to the next smoothly. Time.deltaTime is the difference in the time since the last frame

            if (transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
            if (transform.position.x < leftX) transform.position = new Vector3(leftX, transform.position.y, transform.position.z);
        }
    }
}
