using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

    private gameMaster gm;
    public GameObject player;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gm.lastCheckpointPos = transform.position;
            playerHealth ph = player.GetComponent<playerHealth>();
            PlayerPrefs.SetFloat("StartLevelHealth", ph.currentHealth);
            playerChakra pc = player.GetComponent<playerChakra>();
            PlayerPrefs.SetFloat("StartLevelChakra", pc.currentChakra);
            playerAmmo pa = player.GetComponent<playerAmmo>();
            PlayerPrefs.SetFloat("StartLevelAmmo", pa.currentAmmo);
            Destroy(GetComponent<checkpoint>());
        }
    }
}
