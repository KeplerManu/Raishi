using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float aliveTime;

	// Occurs when object is instantiated
	void Awake () {
        Destroy(gameObject, aliveTime); //destroy kunai after alive time expires
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
