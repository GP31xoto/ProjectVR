using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sound_Script : MonoBehaviour
{
 public GameObject pausemenu;
 public GameObject soundmenu;

 public Slider mySliderGen;
 public Slider mySliderMusic;
 public Slider mySliderSound;

 //Audio
    public AudioSource sfxMenuSounds;
    public AudioClip openMenu;
    public AudioClip select;

 public void PauseMenu()
 {
    playSFX(openMenu);
    pausemenu.SetActive(true);
    soundmenu.SetActive(false);
 }

    public void playSFX(AudioClip clip)
    {
    sfxMenuSounds.clip = clip;
    sfxMenuSounds.Play();
    }
  public void OnGenValueChange()
  {
    playSFX(select);
   if(mySliderGen.value < mySliderMusic.value)
   {
   mySliderMusic.value = mySliderGen.value;         
   }
   else if(mySliderGen.value < mySliderSound.value)
   {
   mySliderMusic.value = mySliderGen.value;
   }

  }

  public void OnMusicValueChange()
  {
    playSFX(select);
   if(mySliderGen.value < mySliderMusic.value)
   {
   mySliderMusic.value = mySliderGen.value;         
   }
 
  }

  public void OnSoundValueChange()
  {
    playSFX(select);
   if(mySliderGen.value < mySliderSound.value)
   {
   mySliderMusic.value = mySliderGen.value;
   }

  }
}
