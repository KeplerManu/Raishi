using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss1EnemyHealth : MonoBehaviour {

    public float enemyMaxHealth;

    float currentHealth;

    public GameObject deathFX;
    public Slider enemyHealthSlider;

    public GameObject player;

    public bool drops;
    public GameObject theDrop;

    Animator enemyAnimator;

    public bool enemySpawner;

    public GameObject spawner;

    public AudioSource[] damageSound;
    public AudioClip[] deathSound;

    // Use this for initialization
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthSlider.maxValue = currentHealth;
        enemyHealthSlider.value = currentHealth;

        enemyAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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

        knockBack();

        if (currentHealth <= 0)
        {
            //if the enemy has enemy spawner boolean as true, then spawn enemies specified in spawnEnemies.cs
            if (enemySpawner)
            {
                enemySpawner spawnEnemies = transform.GetComponent<enemySpawner>();
                spawnEnemies.spawnWave2();
                makeDead();
            }
            else makeDead();
        }
    }

    //get knock backed when damage taken
    void knockBack()
    {
        Transform playerPosition = player.GetComponent<Transform>();
        Vector2 pushDirection = new Vector2(transform.position.x - playerPosition.position.x, 0).normalized; //push character directly away in y direction
        pushDirection *= 10f; //make force greater than 1
        Rigidbody2D pushRB = transform.parent.gameObject.GetComponent<Rigidbody2D>(); //find rigid body of object
        pushRB.velocity = Vector2.zero; //any movement the enemy was making or other forces are set to 0
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse); //and then pushed back
    }

    void makeDead()
    {
        Destroy(gameObject);
        Instantiate(deathFX, transform.position, transform.rotation);
        if (drops) Instantiate(theDrop, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
        //play death sound
        int index = Random.Range(0, deathSound.Length);
        AudioSource.PlayClipAtPoint(deathSound[index], transform.position, 1f + (PlayerPrefs.GetFloat("mySFXVolume") / 40));
        //increase death count for spawner
        spawnConditions spawn = spawner.GetComponent<spawnConditions>();
        spawn.deadSamurai++;
    }
}
