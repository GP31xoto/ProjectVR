using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addResource : MonoBehaviour
{
    public GameObject GameFlow;
    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
        GameFlow.GetComponent<GameFlow>().addResource(this.gameObject.tag);
    }
}
