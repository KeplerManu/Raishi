using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupKunai : MonoBehaviour {

    public float ammoAmount;

    public bool tutorialKunai;
    public GameObject tutorialKunaiScreen;
    public GameObject player;
    public GameObject pauseMenuUI;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
    }

    //pickup kunai
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (tutorialKunai) //show kunai tutorial screen when pick up tutorial kunai
            {
                kunaiPickup(collision);
                Time.timeScale = 0f; //pause game
                tutorialKunaiScreen.SetActive(true); //show tutorial screen
                tutorialController tutorial = player.GetComponent<tutorialController>();
                tutorial.kunaiTutorialActive = true; 
                pauseMenu thePauseMenu = pauseMenuUI.GetComponent<pauseMenu>();
                thePauseMenu.enabled = false; //make it so the player can't pause during the tutorial since game is already paused

                playerController myPC = player.GetComponent<playerController>();
                myPC.audioRun.enabled = false;
                myPC.audioSprint.enabled = false; //disable running and sprinting sounds in tutorial screen
            }
            else
            {
                kunaiPickup(collision); //if normal kunai then just pickup kunai
            }
        }
    }

    //add ammo when kunai picked up
    private void kunaiPickup(Collider2D collision)
    {
        playerAmmo theAmmo = collision.gameObject.GetComponent<playerAmmo>();
        theAmmo.addAmmo(ammoAmount);
        Destroy(gameObject);
    }
}
