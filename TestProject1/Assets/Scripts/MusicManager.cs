using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource m_musicSource;
    public AudioSource m_sfxSource;
    private float m_stingerTime;
    private bool m_playingStinger;
    private float m_remaining;

    public AudioClip m_backgroundMusic;
    public AudioClip m_stinger;
    public AudioClip m_tensionMusic;
    public AudioClip m_disasterSound;
    public AudioClip m_destroyedConstruction;

    // Start is called before the first frame update
    void Start()
    {
        //m_musicSource = this.GetComponent<AudioSource>();
        m_musicSource.clip = m_backgroundMusic;
        m_sfxSource.clip = m_disasterSound;
        m_stingerTime = m_stinger.length;

        m_playingStinger = false;

        m_musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playingStinger)
        {
            m_remaining -= Time.deltaTime;

            if (m_remaining < 0f)
            {
                m_playingStinger = false;
                m_musicSource.clip = m_tensionMusic;
                m_musicSource.loop = true;
                m_musicSource.Play();
            }
        }
    }

    public void PlaySFXDisaster()
    {
        m_sfxSource.clip = m_disasterSound;
        m_sfxSource.Play();
    }

    public void StopSFXDisaster()
    {
        m_sfxSource.Stop();
    }

    public void PlayDeconstruction()
    {
        m_sfxSource.clip = m_destroyedConstruction;
        m_sfxSource.Play();
    }
    public void PlayStinger()
    {
        m_playingStinger = true;
        m_remaining = m_stingerTime;

        m_musicSource.clip = m_stinger;
        m_musicSource.loop = false;
        m_musicSource.Play();
    }

    public void PlayBackground()
    {
        m_playingStinger = false;

        m_musicSource.clip = m_backgroundMusic;
        m_musicSource.Play();
    }

    public void PauseMusic()
    {
        m_musicSource.Pause();
    }

    public void UnpauseMusic()
    {
        m_musicSource.UnPause();
    }
}

