using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth playerFell = collision.GetComponent<playerHealth>();
            playerFell.makeDead();
        }
        else if (collision.tag == "Enemy")
        {
            enemyHealth enemyFell = collision.GetComponent<enemyHealth>();
            enemyFell.makeDead();
        }
    }
}
