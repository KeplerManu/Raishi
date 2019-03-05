using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerAmmo : MonoBehaviour {

    public float maxAmmo;
    public float currentAmmo;

    public TextMeshProUGUI ammoText;
    public GameObject ammoGUI;

    public AudioSource[] ammoSound;

    public gameMaster gm;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            currentAmmo = PlayerPrefs.GetFloat("PlayerCurrentAmmo");
            PlayerPrefs.SetFloat("StartLevelAmmo", currentAmmo);
        }
        else currentAmmo = 0;
    }
	
	// Update is called once per frame
	void Update () {

        PlayerPrefs.SetFloat("PlayerCurrentAmmo", currentAmmo);

        if(currentAmmo > 0)
        {
            ammoGUI.SetActive(true);
            ammoText.text = currentAmmo.ToString();
        }
        else
        {
            ammoGUI.SetActive(false);
        }
	}

    //Add ammo
    public void addAmmo(float ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        //play random ammo receiving sound
        int index = Random.Range(0, ammoSound.Length);
        ammoSound[index].Play();

    }
}
