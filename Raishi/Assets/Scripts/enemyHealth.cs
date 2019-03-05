using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;

    float currentHealth;

    public GameObject deathFX;
    public Slider enemyHealthSlider;

    public GameObject player;

    public bool drops;
    public GameObject theDrop;

    Animator enemyAnimator;

    public bool finalBoss;
    public GameObject winConditions;

    public AudioSource[] damageSound;
    public AudioClip[] deathSound;
    public AudioClip bossDeathSound;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        enemyHealthSlider.maxValue = currentHealth;
        enemyHealthSlider.value = currentHealth;

        enemyAnimator = transform.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        enemyAnimator.SetBool("damageTaken", false);
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;

        enemyHealthSlider.value = currentHealth;

        enemyAnimator.SetBool("damageTaken", true);

        //play random damage sound but not when enemy dies
        if (damage > 0 && currentHealth > 0)
        {
            int index = Random.Range(0, damageSound.Length);
            damageSound[index].Play();
        }

        //every unit but the final boss gets knocked back when damage taken
        if (!finalBoss) knockBack();

        if (currentHealth <= 0) makeDead();
    }

    void knockBack()
    {
        Transform playerPosition = player.GetComponent<Transform>();
        Vector2 pushDirection = new Vector2(transform.position.x - playerPosition.position.x, 0).normalized; //push character directly away in y direction
        pushDirection *= 10f; //make force greater than 1
        Rigidbody2D pushRB = transform.parent.gameObject.GetComponent<Rigidbody2D>(); //find rigid body of object
        pushRB.velocity = Vector2.zero; //any movement the enemy was making or other forces are set to 0
        pushRB.AddForce((pushDirection), ForceMode2D.Impulse); //and then pushed back
    }

    public void makeDead()
    {
        Destroy(gameObject);
        Instantiate(deathFX, transform.position, transform.rotation);
        if (drops) Instantiate(theDrop, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);

        if (finalBoss)
        {
            spawnDoor doorSpawner = winConditions.GetComponent<spawnDoor>();
            doorSpawner.winChecker = true;
            AudioSource.PlayClipAtPoint(bossDeathSound, transform.position, 1f + (PlayerPrefs.GetFloat("mySFXVolume") / 40));
        }
        else
        {
            int index = Random.Range(0, deathSound.Length);
            AudioSource.PlayClipAtPoint(deathSound[index], transform.position, 1f + (PlayerPrefs.GetFloat("mySFXVolume") / 40));
        }
    }
}
