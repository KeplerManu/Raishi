using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;

    public GameObject player;

	// Use this for initialization
	void Start () {
        nextDamage = 0f;

    }
	
	// Update is called once per frame
	void Update () {

	}

    //take damage on collision with player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && nextDamage < Time.time)
        {
            playerHealth health = collision.gameObject.GetComponent<playerHealth>();
            health.addDamage(damage);
            nextDamage = Time.time + damageRate;

            if(gameObject.tag == "Enemy" && health.currentHealth > 0)
            {
                pushBack(collision.transform);
            }
            else
            {
                pushBackSpike(collision.transform);
            }
        }
    }

    //calculate pushback force when damage taken
    void pushBack(Transform pushedObject)
    {
        MonoBehaviour playerMovement = player.GetComponent<MonoBehaviour>();
        playerMovement.enabled = false; //disable playerController.cs
        Vector2 pushDirection = (pushedObject.position - transform.position).normalized; //push character away in y and x directions
        pushDirection *= pushBackForce; //make force greater than 1
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>(); //find rigid body of object
        pushRB.velocity = Vector2.zero; //any movement the player was making or other forces are set to 0
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse); //and then pushed back
        Invoke("reenableMovement", 0.5f); //reenable playerController.cs
    }

    void pushBackSpike(Transform pushedObject)
    {
        Vector2 pushDirection = new Vector2(0, pushedObject.position.y - transform.position.y).normalized; //push character directly away in y direction
        pushDirection *= pushBackForce; //make force greater than 1
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>(); //find rigid body of object
        pushRB.velocity = Vector2.zero; //any movement the player was making or other forces are set to 0
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse); //and then pushed back
    }

    void reenableMovement()
    {
        if (player)
        {
            MonoBehaviour playerMovement = player.GetComponent<MonoBehaviour>();
            playerMovement.enabled = true;
        }
    }
}
