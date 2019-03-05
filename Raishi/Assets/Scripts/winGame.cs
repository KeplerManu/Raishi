using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winGame : MonoBehaviour {

    public GameObject lightningFinish;
    public GameObject sparkEffect;
    gameMaster gm;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Win game when come into contact with object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth playerWins = collision.gameObject.GetComponent<playerHealth>();
            playerWins.winGame();
            Vector3 lightningPosition = new Vector3(transform.position.x, transform.position.y + 3.7f, transform.position.z);
            GameObject.Instantiate(lightningFinish, lightningPosition, transform.rotation);
            
            Instantiate(sparkEffect, transform.position, transform.rotation);

            GetComponent<ParticleSystem>().gameObject.SetActive(false);

            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameMaster>();
                gm.lastCheckpointPos = new Vector2(-0.17f, 1.14f);
            }

            Invoke("Proceed", 1f);
        }
    }

    void Proceed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}