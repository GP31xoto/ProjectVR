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
    private float rateOfGrowth;

    private int turn;//have to check how much time each turn is
    void Start()
    {
        population = 100;
        numDolls = 0;
        turn = 0;
        rateOfGrowth = 1.05f;
        int dollNumber = population / 25;
        spawnDoll(dollNumber);
    }

    void nextTurn()
    {
        turn++;
        if(population < 50)
        {
            endGame();
        }
        int dollNumber = population / 25;
        spawnDoll(dollNumber);
        //do calculations for food,wood,water,and iron
        //if all good just increase pop
        //if water and/or food are not good, kill pop
        //if iron and/or wood are not good , chance for war (which also kills pop)

        float newPop = population*(Mathf.Exp(rateOfGrowth*turn));
        population = (int) newPop;

    }

    void endGame()
    {
        Debug.Log("Not enough POP you lost!");
    }

    void spawnDoll(int numberOfDolls)
    {
        //use this to spawn the dolls
    }
}
