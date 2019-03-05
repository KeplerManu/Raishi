using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class playerController : MonoBehaviour {

    //movement variables
    public float maxSpeed;

    //jumping variables
    bool grounded = false;
    bool touchingWall = false;
    float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform wallCheck1;
    public Transform wallCheck2;
    public bool canWallJump;
    public float jumpHeight;

    //shooting variables
    public Transform throwingHand;
    public GameObject kunai;
    float fireRate = 0.5f;
    float nextFire = 0f;

    //audio variables
    public AudioSource audioRun;
    public AudioSource audioSprint;
    public AudioSource audioThrow;
    public AudioSource audioJump;
    public AudioSource audioAttack;
    public AudioSource audioSwing;

    //attack variables
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask enemies;
    public float meleeDamage;

    //lightning strike variables
    public float attackRangeRadius;
    public Transform lightningFX;
    public float lightningDamage;

    //Chakra Preservation scroll variables
    public bool chakraHalf;

    Rigidbody2D myRB;
    Animator myAnim;
    playerAmmo theAmmo;
    playerChakra theChakra;
    gameMaster gm;
    bool facingRight;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        theAmmo = GetComponent<playerAmmo>();
        theChakra = GetComponent<playerChakra>();

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameMaster>();
        transform.position = gm.lastCheckpointPos;

        if (PlayerPrefs.GetString("ScrollObtained") == "true") chakraHalf = true;

        facingRight = true;

    }

    void Update()
    {
        //jump when touching ground and space is pressed
        if (grounded && Input.GetAxis("Jump") > 0) 
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
            audioJump.Play();
        }

        //jump when touching wall and space is pressed when scroll obtained
        if (touchingWall && Input.GetAxis("Jump") > 0 && canWallJump == true)
        {
            touchingWall = false;
            myAnim.SetBool("isGrounded", grounded);
            if(myRB.velocity.y > 0)
            myRB.AddForce(new Vector2(0, jumpHeight));
            else myRB.AddForce(new Vector2(0, jumpHeight * 2));
            audioJump.Play();
        }

        //player melee
        if (Input.GetAxisRaw("Fire1") > 0) meleeAttack();

        //player shooting
        if (Input.GetAxisRaw("Fire2") > 0 && theAmmo.currentAmmo > 0) throwKunai();

        //player ultimate move
        if (!chakraHalf)
        {
            if (Input.GetKey(KeyCode.E) && theChakra.currentChakra == 100) lightningAttack();
        }
        else
        {
            if (Input.GetKey(KeyCode.E) && theChakra.currentChakra >= 50) lightningAttack();
        }

        //play jump throw animation when kunai is thrown in the air
        if(grounded == false && myAnim.GetBool("kunaiThrown") == true)
        {
            myAnim.Play("playerJumpThrow");
        }

        //play jump melee animation when left click is pressed mid air
        if (grounded == false && myAnim.GetBool("attackClick") == true)
        {
            myAnim.Play("playerJumpAttack");
        }

        //leave tutorials
        if (Input.GetKey(KeyCode.Escape))
        {
            tutorialController tutorialScreen = transform.GetComponent<tutorialController>();
            tutorialScreen.kunaiTutorial();
            tutorialScreen.doubleJumpTutorial();
            tutorialScreen.chakraTutorial();
            tutorialScreen.chakraPreservationScroll();
        }
    }

    // FixedUpdate is called upon a specific amount of time
    void FixedUpdate () {

        //check if grounded. If not, then falling.
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //draw a tiny circle that checks if it's intersecting anything
        touchingWall = Physics2D.OverlapCircle(wallCheck1.position, groundCheckRadius, groundLayer);
        touchingWall = Physics2D.OverlapCircle(wallCheck2.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetBool("kunaiThrown", false);
        myAnim.SetBool("attackClick", false);

        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        float move = Input.GetAxis("Horizontal"); //receive a value between -1 and 1 on horizontal axis 
        myAnim.SetFloat("speed", Mathf.Abs(move)); //gives an abs value of 0 or 1 when button pressed

        if (Input.GetKey(KeyCode.LeftShift)) //If left sheft held down, increase velocity and change animation
        {
            myRB.velocity = new Vector2(move * maxSpeed * 2, myRB.velocity.y);
            myAnim.SetFloat("speed", Mathf.Abs(move) * 10);
        }
        else
        {
            myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);   //instantaneous changes, ignores physics engine and gives game arcadey feel
        }

        if (move > 0 && !facingRight) //if character facing left and right is pressed, flip character
        {
            flip();
        } else if (move < 0 && facingRight)  //if character facing right and left is pressed, flip character
        {
            flip();
        }

        //plays footsteps sounds
        PlayRunFootsteps();
        PlaySprintFootsteps();
    }

    void flip() //flip character in direction that it's moving
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale; //getting scale values from transform
        theScale.x *= -1;                       //and reverse it
        transform.localScale = theScale;
    }

    void meleeAttack() //characters swings his sword
    {
        if (Time.time > nextFire)
        {
            myAnim.SetBool("attackClick", true);
            audioAttack.Play(); // play attack sounds
            audioSwing.Play();
            nextFire = Time.time + fireRate;
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<enemyHealth>())
                enemiesToDamage[i].GetComponent<enemyHealth>().addDamage(meleeDamage);
                else if (enemiesToDamage[i].GetComponent<boss1EnemyHealth>())
                enemiesToDamage[i].GetComponent<boss1EnemyHealth>().addDamage(meleeDamage);
            }
        }
    }

    void throwKunai() //character throws a kunai
    {
        if (Time.time > nextFire)
        {
            audioThrow.Play(); // play throw sound
            nextFire = Time.time + fireRate;
            theAmmo.currentAmmo -= 1;
            if (facingRight)
            {
                Instantiate(kunai, throwingHand.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
                myAnim.SetBool("kunaiThrown", true);
                //avoid axis lock by using Quaternion. Instantiate kunai at the position of the throwing hand towards the right
            }
            else if (!facingRight)
            {
                Instantiate(kunai, throwingHand.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                myAnim.SetBool("kunaiThrown", true);
                //avoid axis lock by using Quaternion. Instantiate kunai at the position of the throwing hand towards the left.
            }
        }
    }

    void lightningAttack() //calls down a bolt of thunder to deal large amounts of damage
    {
        if (Time.time > nextFire)
        {
            myAnim.SetBool("attackClick", true);
            audioAttack.Play(); // play attack sound
            nextFire = Time.time + fireRate;
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Instantiate(lightningFX, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.Euler(new Vector3(0, 0, 0))); //creates lightning bolt where cursor is
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(mousePos), attackRangeRadius, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<enemyHealth>())
                enemiesToDamage[i].GetComponent<enemyHealth>().addDamage(lightningDamage);
                else if (enemiesToDamage[i].GetComponent<boss1EnemyHealth>())
                enemiesToDamage[i].GetComponent<boss1EnemyHealth>().addDamage(lightningDamage);
            }
            if (chakraHalf) theChakra.currentChakra -= 50;
            else theChakra.currentChakra = 0;
        }
    }

    //plays footstep sfx when in motion
    void PlayRunFootsteps()
    {
        if (grounded == true && Input.GetAxis("Horizontal") > 0 || grounded == true && Input.GetAxis("Horizontal") < 0)
        {
            if(Input.GetKey(KeyCode.LeftShift) == false){
                audioSprint.enabled = false;
            }
            audioRun.enabled = true;
            audioRun.loop = true;
        }
        else if (myRB.velocity.x == 0)
        {
            {
                audioRun.enabled = false;
                audioRun.loop = false;
            }
        }
    }

    void StopRunFootSteps()
    {
        audioRun.enabled = false;
        audioRun.loop = false;
    }

    //plays footstep sfx when in sprinting
    void PlaySprintFootsteps()
    {
        if (grounded == true && Input.GetAxis("Horizontal") > 0 && Input.GetKey(KeyCode.LeftShift) == true || grounded == true && Input.GetAxis("Horizontal") < 0 && Input.GetKey(KeyCode.LeftShift) == true)
        {
            StopRunFootSteps();
            audioSprint.enabled = true;
            audioSprint.loop = true;
        }
        else if (grounded == false || myRB.velocity.x == 0)
        {
            {
                StopRunFootSteps();
                audioSprint.enabled = false;
                audioSprint.loop = false;
            }
        }
    }

    private void OnDrawGizmosSelected() //can see the range of melee attack when adjusting
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

}
