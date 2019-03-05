using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupChakra : MonoBehaviour {

    public float chakraAmount;

    public bool tutorialChakra;
    public GameObject tutorialChakraScreen;
    public GameObject pauseMenuUI;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //pickupChakra
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (tutorialChakra) //show chakra tutorial screen when pick up tutorial chakra
            {
                chakraPickup(collision);
                Time.timeScale = 0f; //pause game
                tutorialChakraScreen.SetActive(true); //show tutorial screen
                tutorialController tutorial = player.GetComponent<tutorialController>();
                tutorial.chakraTutorialActive = true;
                pauseMenu thePauseMenu = pauseMenuUI.GetComponent<pauseMenu>();
                thePauseMenu.enabled = false; //make it so the player can't pause during the tutorial since game is already paused

                playerController myPC = player.GetComponent<playerController>();
                myPC.audioRun.enabled = false;
                myPC.audioSprint.enabled = false; //disable running and sprinting sounds in tutorial screen
            }
            else chakraPickup(collision); //if normal chakra then just pickup chakra
        }
    }

    //add chakra when chakra orb picked up
    void chakraPickup(Collider2D collision)
    {
        playerChakra theChakra = collision.gameObject.GetComponent<playerChakra>();
        theChakra.addChakra(chakraAmount);
        Destroy(gameObject);
    }
}
