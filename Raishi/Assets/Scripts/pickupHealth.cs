using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHealth : MonoBehaviour {

    public float healthAmount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //pickup health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth theHealth = collision.gameObject.GetComponent<playerHealth>();
            theHealth.addHealth(healthAmount);
            Destroy(gameObject);
        }
    }
}
