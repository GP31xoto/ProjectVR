using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OVRTouchSample;

public class Hand_Menu : MonoBehaviour
{
    //tabs for each section of the menu: resources, construction and population growth
    [SerializeField] private GameObject resourcesTab;
    [SerializeField] private GameObject constructsTab;
    [SerializeField] private GameObject populationTab;
    //text for each resource and rate of population growth
    [SerializeField] private Text populationCounter;
    [SerializeField] private Text foodCounter;
    [SerializeField] private Text ironCounter;
    [SerializeField] private Text woodCounter;

    //object arrays for resource and construction options and Gameflow object
    private GameObject[] resourceSource;
    private GameObject[] constructType;
    private GameObject GameFlow;

    //ints for running the menu
    private bool isMenuActive;
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
        isMenuActive = false;
        GameFlow = GameObject.FindWithTag("GameFlow");
        //set resource list from options in menu
        resourceSource = new GameObject[resourcesTab.transform.childCount];
        int i = 0;
        foreach (GameObject child in resourcesTab.transform)
        {
            resourceSource[i] = child;
            i++;
        }
        //set construction list from options in menu
        constructType = new GameObject[constructsTab.transform.childCount];
        i = 0;
        foreach (GameObject child in constructsTab.transform)
        {
            constructType[i] = child;
            i++;
        }
        //set max resources that can be built
        foodConstructAvailable = 5;
        woodConstructAvailable = 5;
        ironConstructAvailable = 5;
        //GameObject.SetActive(isMenuActive);
    }

    // Update is called once per frame
    void Update()
    {
        //adjust menu options visibility
        MenuOptionsVisibility();
        //refill resurces to be built after 1 round (determine round time later)
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
        //reset text in textboxes
        UpdateText();
    }

    private void UpdateText()
    {
        //update text to count resources available till next round and current growth rate
        foodCounter.text = foodConstructAvailable + "/5";
        woodCounter.text = woodConstructAvailable + "/5";
        ironCounter.text = ironConstructAvailable + "/5";
        populationCounter.text = "X " + GameFlow.GetComponent<GameFlow>().rateOfGrowth;
    }
    public void ChangeMenuState()
    {
        isMenuActive = !isMenuActive;
        //GameObject.SetActive(isMenuActive);
    }

    public void SelectFood()
    {
        //if there are food sources that can be built
        if (foodConstructAvailable > 0)
        {
            //reduce food sources available
            foodConstructAvailable--;
            //reset counter (replace once round time is established)
            foodTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject foodToPlace = Instantiate(resourceSource[0]);
            //remove eventTrigger from copy
            Destroy(foodToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            foodToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void SelectWood()
    {
        //if there are wood sources that can be built
        if (woodConstructAvailable > 0)
        {
            //reduce wood sources available
            woodConstructAvailable--;
            //reset counter (replace once round time is established)
            woodTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject woodToPlace = Instantiate(resourceSource[1]);
            //remove eventTrigger from copy
            Destroy(woodToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            woodToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void SelectIron()
    {
        //if there are iron sources that can be built
        if (ironConstructAvailable > 0)
        {
            //reduce wood sources available
            ironConstructAvailable--;
            //reset counter (replace once round time is established)
            ironTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject ironToPlace = Instantiate(resourceSource[2]);
            //remove eventTrigger from copy
            Destroy(ironToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            ironToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void SelectHouse()
    {
        //if there are enough resources to build a house
        if (GameFlow.GetComponent<GameFlow>().wood >= 4 && GameFlow.GetComponent<GameFlow>().iron >= 3)
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= 4;
            GameFlow.GetComponent<GameFlow>().iron -= 3;
            //create copy to place in player's hand control
            GameObject houseToPlace = Instantiate(constructType[0]);
            //remove eventTrigger from copy
            Destroy(houseToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            houseToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void SelectGranary()
    {
        //if there are enough resources to build a granary
        if (GameFlow.GetComponent<GameFlow>().wood >= 3 && GameFlow.GetComponent<GameFlow>().iron >= 6)
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= 3;
            GameFlow.GetComponent<GameFlow>().iron -= 6;
            //create copy to place in player's hand control
            GameObject granaryToPlace = Instantiate(constructType[1]);
            //remove eventTrigger from copy
            Destroy(granaryToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            granaryToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void SelectForge()
    {
        //if there are enough resources to build a granary
        if (GameFlow.GetComponent<GameFlow>().wood >= 5 && GameFlow.GetComponent<GameFlow>().iron >= 7)
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= 5;
            GameFlow.GetComponent<GameFlow>().iron -= 7;
            //create copy to place in player's hand control
            GameObject forgeToPlace = Instantiate(constructType[2]);
            //remove eventTrigger from copy
            Destroy(forgeToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            forgeToPlace.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    public void IncreasePopulationGrowth()
    {
        //increase growth rate by 0.05
        GameFlow.GetComponent<GameFlow>().rateOfGrowth += 0.05f;
    }

    public void DecreasePopulationGrowth()
    {
        //if growth rate is greater than 0
        if (GameFlow.GetComponent<GameFlow>().rateOfGrowth > 0)
        {
            //decrease growth rate by 0.05
            GameFlow.GetComponent<GameFlow>().rateOfGrowth -= 0.05f;
        }
    }

    private void MenuOptionsVisibility()
    {
        if(GameFlow.GetComponent<GameFlow>().wood <=0 || GameFlow.GetComponent<GameFlow>().iron <= 0)
        {
            constructsTab.SetActive(false);
            foodCounter.enabled = false;
            woodCounter.enabled = false;
            ironCounter.enabled = false;
        }
        else
        {
            constructsTab.SetActive(true);
            foodCounter.enabled = true;
            woodCounter.enabled = true;
            ironCounter.enabled = true;
        }
        if (GameFlow.GetComponent<GameFlow>().food <= 0)
        {
            populationTab.SetActive(false);
            populationCounter.enabled = false;
        }
        else
        {
            populationTab.SetActive(true);
            populationCounter.enabled = true;
        }

        if (GameFlow.GetComponent<GameFlow>().wood < 4 || GameFlow.GetComponent<GameFlow>().iron < 3)
        {
            constructType[0].SetActive(false);
        }
        else
        {
            constructType[0].SetActive(true);
        }
        if (GameFlow.GetComponent<GameFlow>().wood < 3 || GameFlow.GetComponent<GameFlow>().iron < 6)
        {
            constructType[1].SetActive(false);
        }
        else
        {
            constructType[1].SetActive(true);
        }
        if (GameFlow.GetComponent<GameFlow>().wood < 5 || GameFlow.GetComponent<GameFlow>().iron < 7)
        {
            constructType[2].SetActive(false);
        }
        else
        {
            constructType[2].SetActive(true);
        }

        if (foodConstructAvailable <= 0)
        {
            resourceSource[0].SetActive(false);
            foodCounter.enabled = false;
        }
        else
        {
            resourceSource[0].SetActive(true);
            foodCounter.enabled = true;
        }
        if (woodConstructAvailable <= 0)
        {
            resourceSource[1].SetActive(false);
            woodCounter.enabled = false;
        }
        else
        {
            resourceSource[1].SetActive(true);
            woodCounter.enabled = true;
        }
        if (ironConstructAvailable <= 0)
        {
            resourceSource[2].SetActive(false);
            ironCounter.enabled = false;
        }
        else
        {
            resourceSource[2].SetActive(true);
            ironCounter.enabled = true;
        }
    }
}