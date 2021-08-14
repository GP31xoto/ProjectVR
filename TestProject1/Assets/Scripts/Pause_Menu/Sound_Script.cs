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



 public void PauseMenu()
 {
 pausemenu.SetActive(true);
 soundmenu.SetActive(false);
 }

    
  public void OnGenValueChange()
  {

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

   if(mySliderGen.value < mySliderMusic.value)
   {
   mySliderMusic.value = mySliderGen.value;         
   }
 
  }

  public void OnSoundValueChange()
  {

   if(mySliderGen.value < mySliderSound.value)
   {
   mySliderMusic.value = mySliderGen.value;
   }

  }
}
