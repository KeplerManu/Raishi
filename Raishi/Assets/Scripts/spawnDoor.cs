using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnDoor : MonoBehaviour {

    bool activated = false;
    public bool winChecker;
    public Transform whereToSpawn;
    public GameObject door;

    public GameObject congratulationNPC;
    public GameObject warningNPC;
    public GameObject welcomeNPC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkWin();
        if (SceneManager.GetActiveScene().buildIndex == 3) spawnNPC();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !activated && winChecker)
        {
            activated = true;
            Instantiate(door, whereToSpawn.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void checkWin()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3) winChecker = true;
    }

    void spawnNPC()
    {
        if (winChecker)
        {
            congratulationNPC.SetActive(true);
            warningNPC.SetActive(false);
            welcomeNPC.SetActive(false);
        }
    }
}
