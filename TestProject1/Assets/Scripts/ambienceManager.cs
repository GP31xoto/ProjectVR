using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambienceManager : MonoBehaviour
{
    AudioSource windAudio;//based on how they are in the gameobject
    AudioSource wavesAudio;
    AudioSource birdsAudio;

    public AudioClip wind,waves,chirping;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        windAudio = audios[0];
        wavesAudio = audios[1];
        birdsAudio = audios[2];
        windAudio.clip = wind;
        wavesAudio.clip = waves;
        birdsAudio.clip = chirping;

        windAudio.Play();
        wavesAudio.Play();
        birdsAudio.Play();
    }
}
