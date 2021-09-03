using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHUD : MonoBehaviour
{

    [SerializeField] private GameObject GameFlow;
    [SerializeField] private GameObject foodCounter;
    [SerializeField] private GameObject woodCounter;
    [SerializeField] private GameObject ironCounter;
    [SerializeField] private GameObject waterCounter;
    [SerializeField] private GameObject populationCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFlow.GetComponent<GameFlow>().food >= 40f)
        {
            foodCounter.GetComponentInChildren<Material>().color = Color.green;
        }
        else
        {
            foodCounter.GetComponentInChildren<Material>().color = Color.red;
        }
        foodCounter.transform.localScale = new Vector3(50f * GameFlow.GetComponent<GameFlow>().food, 30, 1);
        if (GameFlow.GetComponent<GameFlow>().wood >= 40f)
        {
            woodCounter.GetComponentInChildren<Material>().color = Color.green;
        }
        else
        {
            woodCounter.GetComponentInChildren<Material>().color = Color.red;
        }
        woodCounter.transform.localScale = new Vector3(50f * GameFlow.GetComponent<GameFlow>().wood, 30, 1);
        if (GameFlow.GetComponent<GameFlow>().iron >= 40f)
        {
            ironCounter.GetComponentInChildren<Material>().color = Color.green;
        }
        else
        {
            ironCounter.GetComponentInChildren<Material>().color = Color.red;
        }
        ironCounter.transform.localScale = new Vector3(50f * GameFlow.GetComponent<GameFlow>().iron, 30, 1);
        if (GameFlow.GetComponent<GameFlow>().water >= 40f)
        {
            waterCounter.GetComponentInChildren<Material>().color = Color.green;
        }
        else
        {
            waterCounter.GetComponentInChildren<Material>().color = Color.red;
        }
        waterCounter.transform.localScale = new Vector3(50f * GameFlow.GetComponent<GameFlow>().water, 30, 1);
        if (GameFlow.GetComponent<GameFlow>().population >= 40)
        {
            populationCounter.GetComponentInChildren<Material>().color = Color.green;
        }
        else
        {
            populationCounter.GetComponentInChildren<Material>().color = Color.red;
        }
        populationCounter.transform.localScale = new Vector3(50f * GameFlow.GetComponent<GameFlow>().food, 30, 1);
    }
}
