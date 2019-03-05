using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class readSign : MonoBehaviour {

    public GameObject signText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //reveal sign message when in range of sign
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TextMeshProUGUI sign = signText.GetComponent<TextMeshProUGUI>();
            sign.enabled = true;
        }
    }

    //gets rid of message when leaving sign boundaries
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TextMeshProUGUI sign = signText.GetComponent<TextMeshProUGUI>();
            sign.enabled = false;
        }
    }
}
