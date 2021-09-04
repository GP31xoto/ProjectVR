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
    [SerializeField] private Text waterCounter;

    //object arrays for resource and construction options and Gameflow object
    private GameObject foodSource;
    private GameObject woodSource;
    private GameObject ironSource;
    private GameObject waterSource;
    private GameObject[] houseType;
    private GameObject[] forgeType;
    private GameObject[] granaryType;
    private GameObject[] workshopType;
    private GameObject GameFlow;

    //ints for running the menu
    private int foodConstructAvailable;
    private int woodConstructAvailable;
    private int ironConstructAvailable;
    private int waterConstructAvailable;
    private int foodTimeCounter;
    private int woodTimeCounter;
    private int ironTimeCounter;
    private int waterTimeCounter;
    private int foodTimeCounterStart;
    private int woodTimeCounterStart;
    private int ironTimeCounterStart;
    private int waterTimeCounterStart;

    //Audio
    private AudioSource musicSource;
    public AudioClip placing;
    public AudioClip select;

    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
        //set resource lists from options in menu
        for (int i = 0; i < resourcesTab.transform.childCount; i++)
        {
            GameObject child = resourcesTab.transform.GetChild(i).gameObject;
            if (child.tag == "Food")
            {
                foodSource = child;
            }
            else if (child.tag == "Wood")
            {
                woodSource = child;
            }
            else if (child.tag == "Iron")
            {
                ironSource = child;
            }
            else if (child.tag == "Water")
            {
                waterSource = child;
            }
        }
        //set construction lists from options in menu
        houseType = new GameObject[6];
        int j = 0;
        forgeType = new GameObject[6];
        int x = 0;
        granaryType = new GameObject[6];
        int y = 0;
        workshopType = new GameObject[6];
        int z = 0;
        for (int i = 0; i < constructsTab.transform.childCount; i++)
        {
            GameObject child = constructsTab.transform.GetChild(i).gameObject;
            if (child.tag == "House")
            {
                houseType[j] = child;
                j++;
            }
            else if (child.tag == "Forge")
            {
                forgeType[x] = child;
                x++;
            }else if (child.tag == "Granary")
            {
                granaryType[y] = child;
                y++;
            }
            else if (child.tag == "Workshop")
            {
                workshopType[z] = child;
                z++;
            }
        }
        //set max resources that can be built
        foodConstructAvailable = 5;
        woodConstructAvailable = 5;
        ironConstructAvailable = 5;
        waterConstructAvailable = 5;
    }

    void playSoundEffect(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //adjust menu options visibility
        MenuOptionsVisibility();
        //refill resurces to be built after 1 round (determine round time later)
        foodTimeCounter = (int)Time.deltaTime - foodTimeCounterStart;
        if (foodTimeCounter > 1000 && foodConstructAvailable < 3)
        {
            foodConstructAvailable++;
            foodTimeCounterStart = (int)Time.deltaTime;
        }
        woodTimeCounter = (int)Time.deltaTime - woodTimeCounterStart;
        if (woodTimeCounter > 1000 && woodConstructAvailable < 3)
        {
            woodConstructAvailable++;
            woodTimeCounterStart = (int)Time.deltaTime;
        }
        ironTimeCounter = (int)Time.deltaTime - ironTimeCounterStart;
        if (ironTimeCounter > 1000 && ironConstructAvailable < 4)
        {
            ironConstructAvailable++;
            ironTimeCounterStart = (int)Time.deltaTime;
        }
        waterTimeCounter = (int)Time.deltaTime - waterTimeCounterStart;
        if (waterTimeCounter > 1000 && waterConstructAvailable < 5)
        {
            waterConstructAvailable++;
            waterTimeCounterStart = (int)Time.deltaTime;
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
        waterCounter.text = waterConstructAvailable + "/5";
        populationCounter.text = "X " + GameFlow.GetComponent<GameFlow>().rateOfGrowth;
    }

    public void SelectFood()
    {
        playSoundEffect(select);
        //if there are food sources that can be built for this type
        if (foodConstructAvailable > 0 && GameFlow.GetComponent<GameFlow>().water > 0)
        {
            //reduce food sources available depending on type
            foodConstructAvailable--;
            //reset counter (replace once round time is established)
            foodTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject foodToPlace = Instantiate(foodSource);
            //remove eventTrigger from copy
            Destroy(foodToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            foodToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectWood()
    {
        playSoundEffect(select);
        //if there are wood sources that can be built for this type
        if (woodConstructAvailable > 0 && GameFlow.GetComponent<GameFlow>().water > 0)
        {
            //reduce wood sources available depending on type
            woodConstructAvailable--;
            //reset counter (replace once round time is established)
            woodTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject woodToPlace = Instantiate(woodSource);
            //remove eventTrigger from copy
            Destroy(woodToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            woodToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectIron()
    {
        playSoundEffect(select);
        //if there are iron sources that can be built for this type
        if (ironConstructAvailable > 0)
        {
            //reduce wood sources available depending on type
            ironConstructAvailable--;
            //reset counter (replace once round time is established)
            ironTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject ironToPlace = Instantiate(ironSource);
            //remove eventTrigger from copy
            Destroy(ironToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            ironToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectWater()
    {
        playSoundEffect(select);
        //if there are water sources that can be built
        if (waterConstructAvailable > 0)
        {
            //reduce wood sources available
            waterConstructAvailable--;
            //reset counter (replace once round time is established)
            waterTimeCounterStart = (int)Time.deltaTime;
            //create copy to place in player's hand control
            GameObject waterToPlace = Instantiate(waterSource);
            //remove eventTrigger from copy
            Destroy(waterToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            waterToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectHouse(int type)
    {
        playSoundEffect(select);
        GameObject houseSelected = houseType[type - 1];
        //if there are enough resources to build a house
        if (GameFlow.GetComponent<GameFlow>().wood >= houseSelected.GetComponent<House>().cost.consumeWood() && GameFlow.GetComponent<GameFlow>().iron >= houseSelected.GetComponent<House>().cost.consumeIron())
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= houseSelected.GetComponent<House>().cost.consumeWood();
            GameFlow.GetComponent<GameFlow>().iron -= houseSelected.GetComponent<House>().cost.consumeIron();
            //create copy to place in player's hand control
            GameObject houseToPlace = Instantiate(houseSelected);
            houseToPlace.transform.localScale = Vector3.one;
            //remove eventTrigger from copy
            Destroy(houseToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            houseToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectGranary(int type)
    {
        playSoundEffect(select);
        GameObject granarySelected = granaryType[type - 1];
        //if there are enough resources to build a granary
        if (GameFlow.GetComponent<GameFlow>().wood >=granarySelected.GetComponent<Granary>().cost.consumeWood() && GameFlow.GetComponent<GameFlow>().iron >= granarySelected.GetComponent<Granary>().cost.consumeIron())
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= granarySelected.GetComponent<Granary>().cost.consumeWood();
            GameFlow.GetComponent<GameFlow>().iron -= granarySelected.GetComponent<Granary>().cost.consumeIron();
            //create copy to place in player's hand control
            GameObject granaryToPlace = Instantiate(granarySelected);
            granaryToPlace.transform.localScale = Vector3.one;
            //remove eventTrigger from copy
            Destroy(granaryToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            granaryToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    public void SelectForge(int type)
    {
        playSoundEffect(select);
        GameObject forgeSelected = forgeType[type - 1];
        //if there are enough resources to build a granary
        if (GameFlow.GetComponent<GameFlow>().wood >= forgeSelected.GetComponent<Forge>().cost.consumeWood() && GameFlow.GetComponent<GameFlow>().iron >= forgeSelected.GetComponent<Forge>().cost.consumeIron())
        {
            //consume resources necessary
            GameFlow.GetComponent<GameFlow>().wood -= forgeSelected.GetComponent<Forge>().cost.consumeWood();
            GameFlow.GetComponent<GameFlow>().iron -= forgeSelected.GetComponent<Forge>().cost.consumeIron();
            //create copy to place in player's hand control
            GameObject forgeToPlace = Instantiate(forgeSelected);
            forgeToPlace.transform.localScale = Vector3.one;
            //remove eventTrigger from copy
            Destroy(forgeToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            forgeToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        }
    }

    //this one will still need heavy editing
    public void SelectWorkshop(int type)
    {
        playSoundEffect(select);
        GameObject workshopSelected = workshopType[type - 1];
        //if there are enough resources to build a workshop (will check on relevant script)
        //if (GameFlow.GetComponent<GameFlow>().wood >= workshopSelected.GetComponent<Workshop>().cost.consumeWood() && GameFlow.GetComponent<GameFlow>().iron >= workshopSelected.GetComponent<Workshop>().cost.consumeIron())
        //{
            //consume resources necessary
            //GameFlow.GetComponent<GameFlow>().wood -= workshopSelected.GetComponent<Workshop>().cost.consumeWood();
            //GameFlow.GetComponent<GameFlow>().iron -= workshopSelected.GetComponent<Workshop>().cost.consumeIron();
            //create copy to place in player's hand control
            GameObject workshopToPlace = Instantiate(workshopSelected);
            workshopToPlace.transform.localScale = Vector3.one;
            //remove eventTrigger from copy
            Destroy(workshopToPlace.GetComponent<EventTrigger>());
            //activate copy as grabbable
            workshopToPlace.GetComponent<OVRGrabbable>().enabled = true;

            playSoundEffect(placing);
        //}
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
        if(GameFlow.GetComponent<GameFlow>().wood <= 0 || GameFlow.GetComponent<GameFlow>().iron <= 0 || GameFlow.GetComponent<GameFlow>().turncost >= 2)
        {
            constructsTab.SetActive(false);
            foodCounter.enabled = false;
            woodCounter.enabled = false;
            ironCounter.enabled = false;
        }
        else
        {
            constructsTab.SetActive(true);
            foreach (GameObject housing in houseType)
            {
                if (GameFlow.GetComponent<GameFlow>().wood < housing.GetComponent<House>().cost.consumeWood() || GameFlow.GetComponent<GameFlow>().iron < housing.GetComponent<House>().cost.consumeIron())
                {
                    housing.SetActive(false);
                }
                else
                {
                    housing.SetActive(true);
                }
            }
            foreach (GameObject forging in forgeType)
            {
                if (GameFlow.GetComponent<GameFlow>().wood < forging.GetComponent<Forge>().cost.consumeWood() || GameFlow.GetComponent<GameFlow>().iron < forging.GetComponent<Forge>().cost.consumeIron())
                {
                    forging.SetActive(false);
                }
                else
                {
                    forging.SetActive(true);
                }
            }
            foreach (GameObject graining in granaryType)
            {
                if (GameFlow.GetComponent<GameFlow>().wood < graining.GetComponent<Granary>().cost.consumeWood() || GameFlow.GetComponent<GameFlow>().iron < graining.GetComponent<Granary>().cost.consumeIron())
                {
                    graining.SetActive(false);
                }
                else
                {
                    graining.SetActive(true);
                }
            }
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

        if (foodConstructAvailable <= 0)
        {
            foodSource.SetActive(false);
            foodCounter.enabled = false;
        }
        else
        {
            if (GameFlow.GetComponent<GameFlow>().water <= 0)
            {
                foodSource.SetActive(false);
                foodCounter.enabled = false;
            }
            else
            {
                foodSource.SetActive(true);
                foodCounter.enabled = true;
            }
        }
        if (woodConstructAvailable <= 0)
        {
            woodSource.SetActive(false);
            woodCounter.enabled = false;
        }
        else
        {
            if (GameFlow.GetComponent<GameFlow>().water <= 0)
            {
                woodSource.SetActive(false);
                woodCounter.enabled = false;
            }
            else
            {
                woodSource.SetActive(true);
                woodCounter.enabled = true;
            }
        }
        if (ironConstructAvailable <= 0)
        {
            ironSource.SetActive(false);
            ironCounter.enabled = false;
        }
        else
        {
            ironSource.SetActive(true);
            ironCounter.enabled = true;
        }
        if (waterConstructAvailable <= 0)
        {
            waterSource.SetActive(false);
            waterCounter.enabled = false;
        }
        else
        {
            waterSource.SetActive(true);
            waterCounter.enabled = true;
        }
    }
}