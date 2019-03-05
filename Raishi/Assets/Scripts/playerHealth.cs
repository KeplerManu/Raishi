using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class playerHealth : MonoBehaviour
{

    public float fullHealth;
    public GameObject deathFX;
    public GameObject lightningFinish;

    public restartGame gameManagerScript;
    public gameMaster gm;

    public float currentHealth;

    //Audio Variable
    public AudioClip deathSound;
    public AudioSource[] damageSound;
    public AudioSource[] healSound;

    //HUD Variables
    public Slider healthSlider;
    public Slider secondaryHealthSlider;
    public GameObject gameOverScreen;
    public Image damageScreen;

    //damage screen variables
    bool damaged = false;
    Color damagedColor = new Color(1f, 1f, 1f, 0.5f);
    float smoothColor = 3f;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentHealth = fullHealth;

            //HUD Initialisation
            healthSlider.maxValue = fullHealth;
            healthSlider.value = fullHealth;
            secondaryHealthSlider.maxValue = fullHealth;
            secondaryHealthSlider.value = fullHealth;
        }
        else
        {
            currentHealth = PlayerPrefs.GetFloat("PlayerCurrentHealth");
            PlayerPrefs.SetFloat("StartLevelHealth", currentHealth);

            //HUD Initialisation
            healthSlider.maxValue = fullHealth;
            healthSlider.value = currentHealth;
            secondaryHealthSlider.maxValue = fullHealth;
            secondaryHealthSlider.value = currentHealth;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerCurrentHealth", currentHealth);

        //show thunder border when damage taken
        if (damaged)
        {
            damageScreen.color = damagedColor;
        }
        //transition border back to invisible
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);
        }
        damaged = false;
    }

    //Add health
    public void addHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        healthSlider.value = currentHealth;
        secondaryHealthSlider.value = currentHealth;
        //play random food eating sound
        int index = Random.Range(0, healSound.Length);
        healSound[index].Play();
    }

    //Damage the character
    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth = currentHealth - damage;
        healthSlider.value = currentHealth;
        secondaryHealthSlider.value = currentHealth;

        //flash damage screen border
        damaged = true;

        //play random damage sound but not when character dies
        if (damage > 0 && currentHealth > 0)
        {
            int index = Random.Range(0, damageSound.Length);
            damageSound[index].Play();
        }

        //kill character when health reaches 0
        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    //instantiate deathFX and destroy character game object and respawn
    public void makeDead()
    {
        currentHealth = 0;
        healthSlider.value = currentHealth;
        secondaryHealthSlider.value = currentHealth;
        Destroy(gameObject);
        gameManagerScript.restartTheGame();

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");

        damageScreen.color = damagedColor;

        Instantiate(deathFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.7f + (PlayerPrefs.GetFloat("mySFXVolume")/40));
    }

    //Character disappears when you win the game
    public void winGame()
    {
        Destroy(gameObject);
    }
}
