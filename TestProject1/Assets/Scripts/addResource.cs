using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addResource : MonoBehaviour
{
    private GameObject GameFlow;
    // Start is called before the first frame update
    void Awake()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
        GameFlow.GetComponent<GameFlow>().addResource(this.gameObject.tag,false);
    }

    public void buildingDestroyed()
    {
        GameFlow.GetComponent<GameFlow>().addResource(this.gameObject.tag,true);
    }
}
