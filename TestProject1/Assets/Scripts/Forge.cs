using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    protected ConstructionCost cost;
    [SerializeField] private int type;
    private float buffIron;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case 1:
                cost = new ConstructionCost(5, false);
                buffIron = 0.5f;
                break;
            case 2:
                cost = new ConstructionCost(4, 2);
                buffIron = 0.7f;
                break;
            case 3:
                cost = new ConstructionCost(3, 7);
                buffIron = 1.2f;
                break;
            case 4:
                cost = new ConstructionCost(6, 6);
                buffIron = 1.5f;
                break;
            case 5:
                cost = new ConstructionCost(8, 3);
                buffIron = 1.6f;
                break;
            case 6:
                cost = new ConstructionCost(7, 9);
                buffIron = 2.0f;
                break;
        }
    }

    public float GiveBuff()
    {
        return buffIron;
    }
}
