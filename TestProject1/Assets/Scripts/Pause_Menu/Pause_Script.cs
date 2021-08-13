using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Script : MonoBehaviour
{
public GameObject pausemenu;
public GameObject soundmenu;


public void Resume()
{
pausemenu.SetActive(false);
}

public void SoundMenu()
{
soundmenu.SetActive(true);
pausemenu.SetActive(false);
}

public void QuitGame()
{
Application.Quit();
}

}
