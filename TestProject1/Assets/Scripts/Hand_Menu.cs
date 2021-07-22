using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Menu : MonoBehaviour
{
    [SerializeField] private GameObject resourcesTab;
    [SerializeField] private GameObject constructsTab;
    [SerializeField] private GameObject populationTab;
    private GameObject[] resourceSource;
    private GameObject[] constructType;

    // Start is called before the first frame update
    void Start()
    {
        resourceSource = new GameObject[resourcesTab.transform.childCount];
        int i = 0;
        foreach (GameObject child in resourcesTab.transform)
        {
            resourceSource[i] = child;
            i++;
        }
        constructType = new GameObject[constructsTab.transform.childCount];
        i = 0;
        foreach (GameObject child in constructsTab.transform)
        {
            constructType[i] = child;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //MenuOptionsVisibility();
    }
    /*
    private void MenuOptionsVisibility()
    {
        if(GameFlow.wood <=0 || GameFlow.iron <= 0)
        {
            constructsTab.setActive = false;
        }
        else
        {
            constructsTab.setActive = true;
        }
        if (GameFlow.food <= 0)
        {
            populationTab.setActive = false;
        }
    }*/
}
