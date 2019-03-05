using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunaiHit : MonoBehaviour {

    public float kunaiDamage;

    projectileController myPC;

    public GameObject sparkEffect;

	// Use this for instantiation
	void Awake () {
        myPC = GetComponentInParent<projectileController>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) //whenever the collider comes into contact with another collider
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //if the collider is on the layer "Shootable", then stop kunai, instantiate spark effect, and destroy kunai sprite
        {
            myPC.removeForce();
            Instantiate(sparkEffect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
            if(other.tag == "Enemy")
            {
                if (other.gameObject.GetComponent<enemyHealth>())
                {
                    enemyHealth damageEnemy = other.gameObject.GetComponent<enemyHealth>();
                    damageEnemy.addDamage(kunaiDamage);
                }
                else if (other.gameObject.GetComponent<boss1EnemyHealth>())
                {
                    boss1EnemyHealth damageEnemy = other.gameObject.GetComponent<boss1EnemyHealth>();
                    damageEnemy.addDamage(kunaiDamage);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) //safeguard. if the object's going to fast we can still catch it
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //if the collider is on the layer "Shootable", then stop kunai, instantiate spark effect, and destroy kunai sprite
        {
            myPC.removeForce();
            Instantiate(sparkEffect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }
}
