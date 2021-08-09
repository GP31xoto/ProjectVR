using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granary : MonoBehaviour
{
    protected ConstructionCost cost;
    [SerializeField] private int type;
    private float buffFood;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case 1:
                cost = new ConstructionCost(3, true);
                buffFood = 0.5f;
                break;
            case 2:
                cost = new ConstructionCost(5, 7);
                buffFood = 0.7f;
                break;
            case 3:
                cost = new ConstructionCost(6, 4);
                buffFood = 1.2f;
                break;
            case 4:
                cost = new ConstructionCost(7, 7);
                buffFood = 1.5f;
                break;
            case 5:
                cost = new ConstructionCost(8, 2);
                buffFood = 1.6f;
                break;
            case 6:
                cost = new ConstructionCost(4, 9);
                buffFood = 2.0f;
                break;
        }
    }

    public float GiveBuff()
    {
        return buffFood;
    }
}
