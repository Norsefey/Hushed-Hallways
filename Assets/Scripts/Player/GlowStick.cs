using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStick : MonoBehaviour
{
    [SerializeField]
    private GameObject glowStick;
    [SerializeField]
    private float throwStrength = 2f;

    Camera cam;

    public float coolDown = 3f;
    float timer = 0;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && timer <= 0)
        {
            var stick = Instantiate(glowStick, cam.transform.position +(cam.transform.right / 2), Quaternion.identity);
            stick.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwStrength, ForceMode.Impulse);

            timer = coolDown;
        }

        if(timer > 0) timer -= Time.deltaTime * 1;

    }
}
