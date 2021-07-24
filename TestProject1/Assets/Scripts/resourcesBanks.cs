using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcesBanks : MonoBehaviour
{
    public GameObject GameFlow;
    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");//doubles default resource number but has a downside
    }
}
