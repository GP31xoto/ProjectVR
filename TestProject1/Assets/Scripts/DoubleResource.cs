using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleResource : MonoBehaviour
{
    private GameObject GameFlow;
    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
        GameFlow.GetComponent<GameFlow>().doubleResource(this.gameObject.tag,false);
    }

    public void buildingDestroyed()
    {
        GameFlow.GetComponent<GameFlow>().doubleResource(this.gameObject.tag,true);
    }
}
