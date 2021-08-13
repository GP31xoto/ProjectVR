using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Script : MonoBehaviour
{
public GameObject pausemenu;
public GameObject soundmenu;

 public void PauseMenu()
{
pausemenu.SetActive(true);
soundmenu.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
