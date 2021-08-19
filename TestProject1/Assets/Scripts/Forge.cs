using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    public ConstructionCost cost;
    [SerializeField] private int type;
    private float buffIron;
    private GameObject GameFlow;

    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
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

    public void GiveBuff()
    {
        GameFlow.GetComponent<GameFlow>().addBuff("Iron",false,buffIron);
    }

    public void removeBuff()
    {
        GameFlow.GetComponent<GameFlow>().addBuff("Iron",true,buffIron);
    }
}
