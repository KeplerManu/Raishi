using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnConditions : MonoBehaviour {

    public int deadSamurai = 0;

    public GameObject spawnObject;
    bool spawnObjectSpawned = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(deadSamurai >= 3 && spawnObjectSpawned == false)
        {
            spawnObjectSpawned = true;
            spawnObject.SetActive(true);
        }
	}
}
