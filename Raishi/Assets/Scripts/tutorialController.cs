using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour {

    //tutorial variables
    public bool kunaiTutorialActive = false;
    public bool chakraTutorialActive = false;
    public bool doubleJumpTutorialActive = false;
    public bool chakraTextActive = false;
    public GameObject kunaiTutorialUI;
    public GameObject chakraTutorialUI;
    public GameObject doubleJumpTutorialUI;
    public GameObject pauseMenuUI;
    public GameObject chakraTextUI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void kunaiTutorial()
    {
        if (kunaiTutorialActive)
        {
            resumeFromTutorial();
            kunaiTutorialActive = false;
            kunaiTutorialUI.SetActive(false);
        }
    }

    public void doubleJumpTutorial()
    {
        if (doubleJumpTutorialActive)
        {
            resumeFromTutorial();
            doubleJumpTutorialActive = false;
            doubleJumpTutorialUI.SetActive(false);
        }
    }

    public void chakraTutorial()
    {
        if (chakraTutorialActive)
        {
            resumeFromTutorial();
            chakraTutorialActive = false;
            chakraTutorialUI.SetActive(false);
        }
    }

    public void chakraPreservationScroll()
    {
        if (chakraTextActive)
        {
            resumeFromTutorial();
            chakraTextActive = false;
            chakraTextUI.SetActive(false);
        }
    }

    private void resumeFromTutorial()
    {
        Time.timeScale = 1f;
        pauseMenu thePauseMenu = pauseMenuUI.GetComponent<pauseMenu>();
        thePauseMenu.enabled = true;
    }
}
