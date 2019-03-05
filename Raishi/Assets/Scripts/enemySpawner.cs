using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject strongSamurai1;
    public GameObject strongSamurai2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnWave2()
    {
        strongSamurai1.SetActive(true);
        strongSamurai2.SetActive(true);
    }
}
