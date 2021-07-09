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
        if(waterDeath == true)//make function
        {
            if(waterCounter == 1){endGame();}
            waterCounter++;          
        }
        else{waterDeath = false; waterCounter=0;}//make function
        if(foodDeath == true)
        {
            if(foodCounter == 3){endGame();}
            foodCounter++;          
        }
        else{foodDeath = false; foodCounter=0;}//make function
        if(population < 50)
        {
            endGame();
        }
        int dollNumber = population / 25;
        spawnDoll(dollNumber);
        if((food - population) < 0)//make function
        {
            int deathPops = Random.Range(20,120);
            population = population - deathPops;
            foodDeath = true;
        }
        if((water - population) < 0)//make function
        {
            int deathPops = Random.Range(40,140);
            population = population - deathPops;
            waterDeath = true;
        }
        if((wood - population) < 0 && (iron - population) < 0)//make function
        {
            int warChance = Random.Range(0,100);
            if(warChance >= 60)
            {
                int deathPops = Random.Range(100,200);
                population = population - deathPops;
            }
        }

        float newPop = population*(Mathf.Exp(rateOfGrowth*turn));
        population = (int) newPop;
        dollNumber = population / 25;
        spawnDoll((dollsSpawned - dollNumber));

    }

    void endGame()
    {
        Debug.Log("Not enough POP you lost!");
    }

    void spawnDoll(int numberOfDolls)
    {
        for(int i = 0; i<numberOfDolls;i++)
        {
            Instantiate(dollPrefab,spawnPoint.transform.position,Quaternion.identity);
        }
    }
}
