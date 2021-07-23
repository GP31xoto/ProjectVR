using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Menu : MonoBehaviour
{
    [SerializeField] private GameObject resourcesTab;
    [SerializeField] private GameObject constructsTab;
    [SerializeField] private GameObject populationTab;
    [SerializeField] private Text populationCounter;
    [SerializeField] private Text foodCounter;
    [SerializeField] private Text ironCounter;
    [SerializeField] private Text woodCounter;
    private GameObject[] resourceSource;
    private GameObject[] constructType;
    private int foodConstructAvailable;
    private int woodConstructAvailable;
    private int ironConstructAvailable;
    private int foodTimeCounter;
    private int woodTimeCounter;
    private int ironTimeCounter;
    private int foodTimeCounterStart;
    private int woodTimeCounterStart;
    private int ironTimeCounterStart;

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
        foodConstructAvailable = 5;
        woodConstructAvailable = 5;
        ironConstructAvailable = 5;
    }

    // Update is called once per frame
    void Update()
    {
        MenuOptionsVisibility();
        foodTimeCounter = (int)Time.deltaTime - foodTimeCounterStart;
        if (foodTimeCounter > 1000 && foodConstructAvailable < 5)
        {
            foodConstructAvailable++;
            foodTimeCounterStart = (int)Time.deltaTime;
        }
        woodTimeCounter = (int)Time.deltaTime - woodTimeCounterStart;
        if (woodTimeCounter > 1000 && woodConstructAvailable < 5)
        {
            woodConstructAvailable++;
            woodTimeCounterStart = (int)Time.deltaTime;
        }
        ironTimeCounter = (int)Time.deltaTime - ironTimeCounterStart;
        if (ironTimeCounter > 1000 && ironConstructAvailable < 5)
        {
            ironConstructAvailable++;
            ironTimeCounterStart = (int)Time.deltaTime;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        foodCounter.text = foodConstructAvailable + "/5";
        woodCounter.text = woodConstructAvailable + "/5";
        ironCounter.text = ironConstructAvailable + "/5";
        populationCounter.text = "X ";
    }

    private void SelectFood()
    {
        if (foodConstructAvailable > 0)
        {
            foodConstructAvailable--;
            foodTimeCounterStart = (int)Time.deltaTime;
        }
    }

    private void SelectWood()
    {
        if (woodConstructAvailable > 0)
        {
            woodConstructAvailable--;
            woodTimeCounterStart = (int)Time.deltaTime;
        }
    }

    private void SelectIron()
    {
        if (ironConstructAvailable > 0)
        {
            ironConstructAvailable--;
            ironTimeCounterStart = (int)Time.deltaTime;
        }
    }
    
    private void MenuOptionsVisibility()
    {
        if(GameFlow.wood <=0 || GameFlow.iron <= 0)
        {
            constructsTab.SetActive(false);
        }
        else
        {
            constructsTab.SetActive(true);
        }
        if (GameFlow.food <= 0)
        {
            populationTab.SetActive(false);
        }
        else
        {
            populationTab.SetActive(true);
        }

        if (GameFlow.wood < 4 || GameFlow.iron < 3)
        {
            constructType[0].SetActive(false);
        }
        else
        {
            constructType[0].SetActive(true);
        }
        if (GameFlow.wood < 3 || GameFlow.iron < 6)
        {
            constructType[1].SetActive(false);
        }
        else
        {
            constructType[1].SetActive(true);
        }
        if (GameFlow.wood < 5 || GameFlow.iron < 7)
        {
            constructType[2].SetActive(false);
        }
        else
        {
            constructType[2].SetActive(true);
        }

        if (foodConstructAvailable <= 0)
        {
            constructsTab.SetActive(false);
        }
        else
        {
            constructsTab.SetActive(true);
        }
        if (woodConstructAvailable <= 0)
        {
            constructsTab.SetActive(false);
        }
        else
        {
            constructsTab.SetActive(true);
        }
        if (ironConstructAvailable <= 0)
        {
            constructsTab.SetActive(false);
        }
        else
        {
            constructsTab.SetActive(true);
        }
    }
}
