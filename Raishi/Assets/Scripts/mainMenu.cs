using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour {

    public AudioMixer MasterVolume;
    public AudioMixer SFXVolume;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    static float currentMusicVolume;
    static float currentSFXVolume;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("myMusicVolume") == 0 && PlayerPrefs.GetFloat("mySFXVolume") == 0)
        {
            MusicVolumeSlider.value = currentMusicVolume;
            SFXVolumeSlider.value = currentSFXVolume;
            PlayerPrefs.SetFloat("myMusicVolume", 0);
            PlayerPrefs.SetFloat("mySFXVolume", 0);
        }
        else
        {
            MusicVolumeSlider.value = PlayerPrefs.GetFloat("myMusicVolume");
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("mySFXVolume");
        }
    }

    //play game
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    //set music volume
    public void SetMusicVolume(float volume)
    {
        MasterVolume.SetFloat("volume", volume);
        currentMusicVolume = MusicVolumeSlider.value;
        PlayerPrefs.SetFloat("myMusicVolume", volume);
    }

    //set SFX volume
    public void SetSFXVolume(float volume)
    {
        SFXVolume.SetFloat("volume", volume);
        currentSFXVolume = SFXVolumeSlider.value;
        PlayerPrefs.SetFloat("mySFXVolume", volume);
    }
}
