using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {

    public float kunaiSpeed;

    Rigidbody2D myRB;


	// Occurs when an object comes to life
	void Awake () {
        myRB = GetComponent<Rigidbody2D>();
        if (transform.localRotation.z > 0)
        {
            //Instantaneous force applied and object is propelled away from character. Leftwards if kunai is facing left, rightward if right
            myRB.AddForce(new Vector2(1, 0) * kunaiSpeed, ForceMode2D.Impulse);
        }
        else
        { 
            myRB.AddForce(new Vector2(-1, 0) * kunaiSpeed, ForceMode2D.Impulse);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeForce() //change the velocity of the projectile to 0
    {
        myRB.velocity = new Vector2(0, 0);
    }
}
