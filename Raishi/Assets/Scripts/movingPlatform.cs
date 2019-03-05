using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

    public Transform posA;
    public Transform posB;
    private Vector3 nextPos;
    public float platformSpeed;
    public Transform platform;

	// Use this for initialization
	void Start () {
        nextPos = posB.position;
	}
	
	// Update is called once per frame
	void Update () {
        movePlatform();
	}

    //move platform back and forth between two positions
    private void movePlatform()
    {
        platform.position = Vector3.MoveTowards(platform.position, nextPos, platformSpeed * Time.deltaTime);
        if (platform.position == posB.position)
            nextPos = posA.position;
        else if (platform.position == posA.position)
            nextPos = posB.position;

    }

    //make it so player doesn't fall off platform if not moving
    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
