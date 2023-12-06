using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{//player can turn their flashlight on and off
    [Header("References")]
    [SerializeField]//reference to flashlight
    private GameObject flashlight;
    [SerializeField]
    private Light raysOfDelight;
    private bool flashOn = true;

    [Header("Light Intensity")]
    public float defaultIntensity = 1;
    public float minIntensity = .5f;
    public float maxIntensity = 1.5f;

    [Header("Light Angle")]
    public float maxAngle = 60f;
    public float minAngle = 25f;

    // Update is called once per frame
    void Update()
    {
        //when player inputs flashlight with turn off or on depending if it is on/off
        if (Input.GetKeyDown(KeyCode.Q) && flashlight != null)
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
            //use scroll wheel to change angle of light
            raysOfDelight.spotAngle += Input.mouseScrollDelta.y;
            raysOfDelight.spotAngle = Mathf.Clamp(raysOfDelight.spotAngle, minAngle, maxAngle);

            //depending on the angle of the light, change the intensity
            if (raysOfDelight.spotAngle <= 30)
                raysOfDelight.intensity = maxIntensity;
            else if (raysOfDelight.spotAngle >= 50)
                raysOfDelight.intensity = minIntensity;
            else
                raysOfDelight.intensity = defaultIntensity;
        }
    }
}
