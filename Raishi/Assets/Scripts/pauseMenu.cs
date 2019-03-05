using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{

    public AudioMixer MasterVolume;
    public AudioMixer sfxVolume;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    float setVolume;
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject player;

    private void Start()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("myMusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("mySFXVolume");
        MasterVolume.SetFloat(("volume"), MusicVolumeSlider.value);
        sfxVolume.SetFloat("volume", SFXVolumeSlider.value);

    }

    private void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    //resume game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gamePaused = false;

        playerController myPC = player.GetComponent<playerController>();
        myPC.enabled = true;
    }

    //pause game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
    
        Time.timeScale = 0f;
        gamePaused = true;

        playerController myPC = player.GetComponent<playerController>();
        myPC.enabled = false;
        myPC.audioRun.enabled = false;
        myPC.audioSprint.enabled = false; //disable character sounds and movement
    }

    //quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    //set volume
    public void SetMusicVolume(float volume)
    {
        MasterVolume.SetFloat(("volume"), volume);
        PlayerPrefs.SetFloat("myMusicVolume", volume);
    }

    //set sfx volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("mySFXVolume", volume);
    }

    //show menu
    public void LoadOptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    //return from options menu to pause menu
    public void ReturnToPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }
}
