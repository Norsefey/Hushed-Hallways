using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public int plushiesCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plushy"))
        {
            plushiesCollected++;
            Destroy(other.gameObject);
        }
    }
}
