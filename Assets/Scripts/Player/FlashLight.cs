using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{//player can turn their flashlight on and off
    [SerializeField]//refrence to flashlight
    private GameObject flashlight;

    private bool flashOn = true;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && flashlight != null)//when player inputs flash light with turn off or on depending if it on/off
        {
            if (flashOn)
            {
                flashlight.SetActive(false);
                flashOn = false;
            }
            else
            {
                flashlight.SetActive(true);
                flashOn = true;
            }
            
        }
    }
}
