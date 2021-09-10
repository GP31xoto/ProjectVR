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
    public int turncost;
    public GameObject doll;

    void Start()
    {
        musicManager = this.GetComponent<MusicManager>();
        turncost = 2;
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
        turncost = 0;
        updateResources();
        counter(waterDeath,foodDeath);
        if(population < 50)
        {
            endGame();
        }
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
                int numOfDolls = population / 25;
                deSpawnDoll(numOfDolls, 3);
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
                int numOfDolls = population / 25;
                deSpawnDoll(numOfDolls, 4);
                warBool = true;
            }
            else{warBool = false; }
            GameObject[] dollsAtWar = GameObject.FindGameObjectsWithTag("Doll");
            foreach (GameObject person in dollsAtWar)
            {
                person.GetComponent<EnemyAI>().atWar = warBool;
            }
        }
    }
    void checkFood(float i)
    {
        if((i) < 0)
        {
            int deathPops = (int)(Random.Range(50,100)/(1+popBuff));
            population = population - deathPops;
            int numOfDolls = population / 25;
            deSpawnDoll(numOfDolls, 2);
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
            int numOfDolls = population / 25;
            deSpawnDoll(numOfDolls, 1);
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
            float distanceZ = Random.Range( -5f, 5f );
            float distanceX = Random.Range( -5f, 5f );
            Vector3 spawnPosition = new Vector3((spawnPoint.transform.position.x *distanceX),spawnPoint.transform.position.y,(spawnPoint.transform.position.z * distanceZ));
            GameObject dollInstance = Instantiate(dollPrefab,spawnPosition,Quaternion.identity);
            dollInstance.GetComponent<OVRGrabbable>().enabled = true;
        }
    }

    void deSpawnDoll(int numOfDolls, int deathType)
    {
        int numDollsToKill = Mathf.Abs(dollsSpawned - numOfDolls);
        for(int i = 0; i<numDollsToKill;i++)
        {
            dollsSpawned--;
            doll = GameObject.FindWithTag("Doll");
            doll.GetComponent<EnemyAI>().death(deathType);
        }
    }

    public void addResource(string resource,bool reverse)
    {
        if(reverse)
        {
            musicManager.PlayDeconstruction();
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
            turncost++;
        }

    }

    public void addBuff(string resource,bool reverse,float buff)
    {
        if(reverse)
        {
            musicManager.PlayDeconstruction();
            if(resource == "Pop"){popBuff -= buff;}
            else if(resource == "Food"){foodBuff -= buff;}
            else if(resource == "Iron"){ironBuff -= buff;}
        }
        else
        {
            if(resource == "Pop"){popBuff += buff;}
            else if(resource == "Food"){foodBuff += buff;}
            else if(resource == "Iron"){ironBuff += buff;}
            turncost++;
        }
    }

    public void doubleResource(string resource,bool reverse)
    {
        if(reverse)
        {
            musicManager.PlayDeconstruction();
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
            turncost++;
        }
    }

    public void resourceBanks(bool reverse)
    {
        if(reverse)
        {
            musicManager.PlayDeconstruction();
            happiness = happiness * 2;
            defaultResourceNumber /= 2;
        }
        else
        {
            happiness = happiness / 2;
            defaultResourceNumber *= 2;
            turncost++;
        }
    }

    public void workShopBuilt(bool reverse)
    {
        if(reverse)
        {
            musicManager.PlayDeconstruction();
            happiness /= 2;
            woodAdd += 100;
            ironAdd += 100;
        }
        else
        {
            happiness *= 2;
            woodAdd -= 100;
            ironAdd -= 100;
            turncost++;
        }
    }

    public void Update()
    {
        if(turncost == 2)
        {

            if (Input.GetKeyDown("space"))
            {
                nextTurn();
            }
        }      
    }
}