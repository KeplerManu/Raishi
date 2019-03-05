using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerChakra : MonoBehaviour {

    public float maxChakra;
    public float currentChakra;

    public Slider primaryHealthBar;
    public Slider secondaryHealthBar;
    public Slider chakraBar;

    public AudioSource receiveChakraSound;

    public gameMaster gm;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //HUD initialization
            chakraBar.maxValue = maxChakra;
            chakraBar.value = currentChakra;
            PlayerPrefs.SetString("ScrollObtained", "false");

        }
        else
        {
                //HUD initialization
                chakraBar.maxValue = maxChakra;
                chakraBar.value = PlayerPrefs.GetFloat("PlayerCurrentChakra");
                currentChakra = PlayerPrefs.GetFloat("PlayerCurrentChakra");
                PlayerPrefs.SetFloat("StartLevelChakra", currentChakra);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerCurrentChakra", currentChakra);

        chakraBar.value = currentChakra;
        if (currentChakra > 0)
        {
            primaryHealthBar.gameObject.SetActive(false);
            secondaryHealthBar.gameObject.SetActive(true);
            chakraBar.gameObject.SetActive(true);
        }
        else
        {
            primaryHealthBar.gameObject.SetActive(true);
            secondaryHealthBar.gameObject.SetActive(false);
            chakraBar.gameObject.SetActive(false);
        }
    }

    //Add chakra
    public void addChakra(float chakraAmount)
    {
        currentChakra += chakraAmount;
        if (currentChakra > maxChakra) currentChakra = maxChakra;
        receiveChakraSound.Play();

    }
}
