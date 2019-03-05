using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupChakraScroll : MonoBehaviour {

    public GameObject player;
    public GameObject chakraText;
    public GameObject pauseMenuUI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //activate double jump function and show double jump tutorial
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0f; //pause game
            chakraText.SetActive(true); //show tutorial screen
            playerController myPC = player.GetComponent<playerController>();
            myPC.chakraHalf = true;
            tutorialController tutorial = player.GetComponent<tutorialController>();
            tutorial.chakraTextActive = true;
            pauseMenu thePauseMenu = pauseMenuUI.GetComponent<pauseMenu>();
            thePauseMenu.enabled = false; //make it so the player can't pause during the tutorial since game is already paused
            Destroy(gameObject);

            myPC.audioRun.enabled = false;
            myPC.audioSprint.enabled = false; //disable running and sprinting sounds in tutorial screen

            PlayerPrefs.SetString("ScrollObtained", "true");
        }
    }
}
