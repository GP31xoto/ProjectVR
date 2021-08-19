using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public ConstructionCost cost;
    [SerializeField] private int type;
    private float buffPopulation;
    private GameObject GameFlow;

    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
        switch (type)
        {
            case 1:
                cost = new ConstructionCost(5, true);
                buffPopulation = 0.05f;
                break;
            case 2:
                cost = new ConstructionCost(3, 4);
                buffPopulation = 0.07f;
                break;
            case 3:
                cost = new ConstructionCost(4, 2);
                buffPopulation = 0.1f;
                break;
            case 4:
                cost = new ConstructionCost(5, 5);
                buffPopulation = 0.14f;
                break;
            case 5:
                cost = new ConstructionCost(7, 3);
                buffPopulation = 0.16f;
                break;
            case 6:
                cost = new ConstructionCost(6, 8);
                buffPopulation = 0.2f;
                break;
        }
    }

    public void GiveBuff()
    {
        GameFlow.GetComponent<GameFlow>().addBuff("Pop",false,buffPopulation);
    }

    public void removeBuff()
    {
        GameFlow.GetComponent<GameFlow>().addBuff("Pop",true,buffPopulation);
    }
}
