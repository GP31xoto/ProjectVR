using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    protected ConstructionCost cost;
    [SerializeField] private int type;
    public float effect;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case 1:
                cost = new ConstructionCost(5, true);
                effect = 0.05f;
                break;
            case 2:
                cost = new ConstructionCost(3, 4);
                effect = 0.07f;
                break;
            case 3:
                cost = new ConstructionCost(4, 2);
                effect = 0.1f;
                break;
            case 4:
                cost = new ConstructionCost(5, 5);
                effect = 0.14f;
                break;
            case 5:
                cost = new ConstructionCost(7, 3);
                effect = 0.16f;
                break;
            case 6:
                cost = new ConstructionCost(6, 8);
                effect = 0.2f;
                break;
        }
    }

    public float GiveEffect()
    {
        return effect;
    }
}
