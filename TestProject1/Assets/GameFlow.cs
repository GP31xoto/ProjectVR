using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public float food;
    public float water;
    public float wood;
    public float iron;
    public int population;
    private int numDolls;
    private int waterCounter;
    private int foodCounter;
    private float rateOfGrowth;
    private bool foodDeath;
    private bool waterDeath;
    public GameObject dollPrefab;
    public GameObject spawnPoint;
    private int dollsSpawned;
    private int turn;//have to check how much time each turn is
    void Start()
    {
        foodDeath = false;
        waterDeath = false;
        population = 100;
        numDolls = 0;
        turn = 0;
        rateOfGrowth = 1.05f;
        dollsSpawned = 0;
        int dollNumber = population / 25;
        spawnDoll(dollNumber);
    }

    void nextTurn()
    {
        turn++;
        counter(waterDeath,foodDeath);
        if(population < 50)
        {
            endGame();
        }
        int dollNumber = population / 25;
        spawnDoll(dollNumber);
        checkFood(food - population);
        checkWater(water - population);
        checkWaI((wood - population),(iron - population));

        float newPop = population*(Mathf.Exp(rateOfGrowth*turn));
        population = (int) newPop;
        dollNumber = population / 25;
        spawnDoll((dollsSpawned - dollNumber));

    }

    void checkWaI(float w, float i)
    {
        if(w < 0 && i < 0)
        {
            int warChance = Random.Range(0,100);
            if(warChance >= 60)
            {
                int deathPops = Random.Range(100,200);
                population = population - deathPops;
            }
        }
    }
    void checkFood(float i)
    {
        if((i) < 0)
        {
            int deathPops = Random.Range(20,120);
            population = population - deathPops;
            foodDeath = true;
        }
        else{foodDeath = false;}
    }

    void checkWater(float i)
    {
        if((i) < 0)
        {
            int deathPops = Random.Range(40,140);
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

    public void addResource(string resource, float resourceAdd)
    {
        if(resource == "Water"){water += resourceAdd;}
        else if(resource == "Food"){food += resourceAdd;}
        else if(resource == "Wood"){wood += resourceAdd;}
        else if(resource == "Iron"){iron += resourceAdd;}
    }
}
