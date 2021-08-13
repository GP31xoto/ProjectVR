using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambienceManager : MonoBehaviour
{
    private AudioSource musicSource;

    public AudioClip wind,waves,chirping;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = this.GetComponent<AudioSource>();
      //  musicSource.clip = m_backgroundMusic;

      //  musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //use three audiosources? or have multiple ambience managers
    }
}
