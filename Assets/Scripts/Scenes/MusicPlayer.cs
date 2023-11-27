using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{//plays assgined background music on loop
    
    [SerializeField]
    private AudioClip backgroundMusic;//what music to play

    private AudioSource speakers;

    // Start is called before the first frame update
    void Start()
    {
        speakers = GetComponent<AudioSource>();

        speakers.clip = backgroundMusic;//assgin music to play
        speakers.Play();
    }

    private void Update()
    {
        if (!speakers.isPlaying)//loops music when it is done playing
        {
            speakers.Play();
        }
    }

}
