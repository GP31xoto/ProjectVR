using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public float food, water, wood, iron;
    private float foodAdd, waterAdd, woodAdd, ironAdd,foodBuff,ironBuff,popBuff = 0;
    public int numFood,numWater,numWood,numIron = 0;
    public float defaultResourceNumber;
    public int population;
    private int numDolls;
    private int waterCounter,foodCounter;
    public float rateOfGrowth;
    public float happiness;
    private bool foodDeath, waterDeath, warBool, unhappyBool;
    public GameObject dollPrefab;
    public GameObject spawnPoint;
    private int dollsSpawned;
    private MusicManager musicManager;
    //add new variable called happiness, low happiness causes people to revolt or leave
    //low happiness causes low growth and high hapiness causes more growth
    private int turn;//have to check how much time each turn is
    private int turncost;

    void Start()
    {
        musicManager = this.GetComponent<MusicManager>();
        turncost = 5;
        happiness = 50;
        defaultResourceNumber = 100;
        foodDeath = false;
        waterDeath = false;
        unhappyBool = false;
        warBool =  false;
        population = 100;
        numDolls = population / 25;
        turn = 0;
        rateOfGrowth = 1.05f;
        dollsSpawned = 0;
        spawnDoll(numDolls);

        Debug.Log(numWood);
    }

    void nextTurn()
    {
        musicManager.StopSFXDisaster();
        turn++;
        updateResources();
        counter(waterDeath,foodDeath);
        if(population < 50)
        {
            endGame();
        }
        numDolls = population / 25;
        spawnDoll(numDolls);
        checkFood(food - population);
        checkWater(water - population);
        checkWaI((wood - population),(iron - population));
        checkHappiness(happiness);
        playBackground(warBool,foodDeath,waterDeath,unhappyBool);

        float newPop = population*(Mathf.Exp(rateOfGrowth*turn));
        population = (int) newPop;
        numDolls = population / 25;
        int numDollsToSpawn = dollsSpawned - numDolls;
        spawnDoll((numDollsToSpawn));

    }

    void updateResources()
    {
        food = (numFood + foodBuff) * (defaultResourceNumber + foodAdd);
        water = numWater *  (defaultResourceNumber + waterAdd);
        wood = numWood *  (defaultResourceNumber + woodAdd);
        iron = (numIron + ironBuff) *  (defaultResourceNumber + ironAdd);
    }

    void playBackground(bool war, bool food, bool water, bool unhappy)
    {
        if(war || water || food || unhappy)
        {
            musicManager.PlaySFXDisaster();
            musicManager.PlayStinger();
        }
        else{musicManager.PlayBackground();}
    }

    void checkHappiness(float h)
    {
        if(h < 0)
        {
            int rebelChance = Random.Range(0,100);
            if(rebelChance >= 60)
            {
                int deathPops = (int)(Random.Range(100,200)/(1+popBuff));
                population = population - deathPops;
                warBool = true;
            }
            else{unhappyBool = false;}
        }
    }

    void checkWaI(float w, float i)
    {
        if(w < 0 && i < 0)
        {
            int warChance = Random.Range(0,100);
            if(warChance >= 60)
            {
                int deathPops = (int)(Random.Range(100,200)/(1+popBuff));
                population = population - deathPops;
                warBool = true;
            }
            else{warBool = false;}
        }
    }
    void checkFood(float i)
    {
        if((i) < 0)
        {
            int deathPops = (int)(Random.Range(50,100)/(1+popBuff));
            population = population - deathPops;
            foodDeath = true;
        }
        else{foodDeath = false;}
    }

    void checkWater(float i)
    {
        if((i) < 0)
        {
            int deathPops = (int)(Random.Range(70,170)/(1+popBuff));
            population = population - deathPops;
            waterDeath = true;
        }
        else{waterDeath = false;}
    }
    void counter(bool water, bool food)
    {
        if(water == true)
        {
            if(waterCounter == 1){endGame();}
            waterCounter++;          
        }
        else{waterCounter=0;}
        if(food == true)
        {
            if(foodCounter == 3){endGame();}
            foodCounter++;          
        }
        else{foodCounter=0;}
    }

    void endGame()
    {
        if(waterCounter == 1||foodCounter == 3)
        {
            Debug.Log("Not enough food or water you Lost!");
            //application quit or just disable the script? or boot back to menu
        }
        Debug.Log("Not enough POP you lost!");
    }

    void spawnDoll(int numberOfDolls)
    {
        for(int i = 0; i<numberOfDolls;i++)
        {
            dollsSpawned++;
            Instantiate(dollPrefab,spawnPoint.transform.position,Quaternion.identity);
        }
    }

    public void addResource(string resource,bool reverse)
    {
        if(reverse)
        {
            if(resource == "Water"){waterAdd -= 100;}
            else if(resource == "Food"){foodAdd -= 100;}
            else if(resource == "Wood"){woodAdd -= 100;}
            else if(resource == "Iron"){ironAdd -= 100;}
        }
        else
        {
            if(resource == "Water"){waterAdd += 100;}
            else if(resource == "Food"){foodAdd += 100;}
            else if(resource == "Wood"){woodAdd += 100;}
            else if(resource == "Iron"){ironAdd += 100;}
        }
    }

    public void addBuff(string resource,bool reverse,float buff)
    {
        if(reverse)
        {
            if(resource == "Pop"){popBuff -= buff;}
            else if(resource == "Food"){foodBuff -= buff;}
            else if(resource == "Iron"){ironBuff -= buff;}
        }
        else
        {
            if(resource == "Pop"){popBuff += buff;}
            else if(resource == "Food"){foodBuff += buff;}
            else if(resource == "Iron"){ironBuff += buff;}
        }
    }

    public void doubleResource(string resource,bool reverse)
    {
        if(reverse)
        {
            if(resource == "Water"){numWater /= 2;}//the only time the num changes is here
            else if(resource == "Food"){numFood /= 2;}
            else if(resource == "Wood"){numWood /= 2;}
            else if(resource == "Iron"){numIron /= 2;}
        }
        else
        {
            if(resource == "Water"){numWater *= 2;}//the only time the num changes is here
            else if(resource == "Food"){numFood *= 2;}
            else if(resource == "Wood"){numWood *= 2;}
            else if(resource == "Iron"){numIron *= 2;}
        }
    }

    public void resourceBanks(bool reverse)
    {
        if(reverse)
        {
            happiness = happiness * 2;
            defaultResourceNumber /= 2;
        }
        else
        {
            happiness = happiness / 2;
            defaultResourceNumber *= 2;
        }
    }

    public void workShopBuilt(bool reverse)
    {
        if(reverse)
        {
            happiness /= 2;
            woodAdd += 100;
            ironAdd += 100;
        }
        else
        {
            happiness *= 2;
            woodAdd -= 100;
            ironAdd -= 100;
        }

    }

    public void Update()
    {
        /*
        if()
        {
            turncost--;
            if(turncost <= 0)
            {
                nextTurn()
                turncost = 5;
            }
        }
        */
    }
}
