using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] plushies;

    private void Start()
    {
        int plushIndex = Random.Range(0, plushies.Length);
        
        Instantiate(plushies[plushIndex], transform.position, transform.rotation);
    }

}
