using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //go to main menu
    public void goToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
