using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementController : MonoBehaviour {

    public float enemySpeed;

    Animator enemyAnimator;

    //facing
    public GameObject enemySprite;
    bool canFlip = true;
    public bool facingRight = false;
    float flipTime = 4f;
    float nextFlipChance = 1f;

    //attacking
    public float chargeTime; //aggression time
    float startChargeTime;
    bool charging;
    Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Shootable"), LayerMask.NameToLayer("Shootable"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Shootable"), LayerMask.NameToLayer("Trap"));
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFlipChance) //every 5 seconds there's a chance the enemy might flip
        {
            if (Random.Range(0, 10) >= 5) flipFacing();
            nextFlipChance = Time.time + flipTime;
        }
	}

    void flipFacing() //flip character
    {
        if (!canFlip) return;
        float facingX = enemySprite.transform.localScale.x;
        facingX *= -1f;
        enemySprite.transform.localScale = new Vector3(facingX, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision) //flip sprite towards player when they enter danger zone box collider
    {
        if (collision.tag == "Player")
        {
            if(facingRight && collision.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if(!facingRight && collision.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            
            startChargeTime = Time.time + chargeTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //start charging at character when they are within danger zone box collider
    {
        if(collision.tag == "Player")
        {
            if (startChargeTime < Time.time)
            {
                if (!facingRight)
                {
                    enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
                    charging = true;
                    enemyAnimator.SetBool("isCharging", charging);
                }
                else
                {
                    enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
                    charging = true;
                    enemyAnimator.SetBool("isCharging", charging);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", charging);
        }
    }
}
