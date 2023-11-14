using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject[] decor;

    private void Start()
    {
        int decorIndex = Random.Range(0, decor.Length);
        int posIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(decor[decorIndex], spawnPoints[decorIndex].position, spawnPoints[decorIndex].rotation);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
