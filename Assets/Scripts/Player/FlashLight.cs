using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{//player can turn their flashlight on and off
    [SerializeField]//refrence to flashlight
    private GameObject flashlight;
    [SerializeField]
    private Light light;
    private bool flashOn = true;

    public float lightMultiplier = 1.0f;
  
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

        if(Input.mouseScrollDelta.y != 0)
        {
            light.spotAngle += Input.mouseScrollDelta.y * lightMultiplier;

            light.spotAngle = Mathf.Clamp(light.spotAngle, 25, 60);


            if (light.spotAngle <= 30)
                light.intensity = 1.5f;
            else if (light.spotAngle >= 50)
                light.intensity = .5f;
            else
                light.intensity = 1f;
        }
       

    }
}
