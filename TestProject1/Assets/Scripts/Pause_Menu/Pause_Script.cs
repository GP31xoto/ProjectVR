using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Script : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject soundmenu;
    public GameObject startmenu;
    public GameObject CanvasHUD;


    //Audio
    public AudioSource menuMusic;
    public AudioSource sfxMenuSounds;
    public AudioClip menuBackground;
    public AudioClip select;
    public AudioClip openMenu;
    private GameObject GameFlow;

public void Start(){
    Time.timeScale = 0;
    GameFlow = GameObject.FindWithTag("GameFlow");
    menuMusic.clip = menuBackground;
}

public void onEnable()//when the menu is opened
{
    pausemenu.SetActive(true);
    Time.timeScale = 0;
    GameFlow.GetComponent<MusicManager>().PauseMusic();
    playSFX(openMenu);
    menuMusic.Play();
}

public void playSFX(AudioClip clip)
{
    sfxMenuSounds.clip = clip;
    sfxMenuSounds.Play();
}
public void Resume()
{
    Time.timeScale = 1;
    playSFX(select);
    menuMusic.Stop();
    GameFlow.GetComponent<MusicManager>().UnpauseMusic();
    pausemenu.SetActive(false);
}

public void SoundMenu()
{
    playSFX(openMenu);
    soundmenu.SetActive(true);
    pausemenu.SetActive(false);
}

public void QuitGame()
{
    playSFX(select);
    Application.Quit();
}

    public void startGame(){
        startmenu.SetActive(false);
        CanvasHUD.SetActive(true);
        Time.timeScale = 1;
    }

}
