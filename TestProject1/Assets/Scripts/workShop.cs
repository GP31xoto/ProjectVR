using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workShop : MonoBehaviour
{
    public GameObject GameFlow;
    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");//take resources but add happiness
    }
}
