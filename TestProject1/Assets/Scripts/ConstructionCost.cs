using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionCost
{
    protected int wood;
    protected int iron;

    public ConstructionCost(int woodCost, int ironCost)
    {
        wood = woodCost;
        iron = ironCost;
    }

    public ConstructionCost(int cost, bool isWood)
    {
        if (isWood)
        {
            wood = cost;
            iron = 0;
        }
        else
        {
            wood = 0;
            iron = cost;
        }
    }

    public int consumeWood()
    {
        return wood;
    }

    public int consumeIron()
    {
        return iron;
    }
}
