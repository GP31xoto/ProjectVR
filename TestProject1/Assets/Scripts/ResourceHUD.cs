using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHUD : MonoBehaviour
{

    [SerializeField] private GameObject GameFlow;
    [SerializeField] private GameObject foodCounter;
    [SerializeField] private Material foodCounterMaterial;
    [SerializeField] private Text foodCounterNumber;
    [SerializeField] private GameObject woodCounter;
    [SerializeField] private Material woodCounterMaterial;
    [SerializeField] private Text woodCounterNumber;
    [SerializeField] private GameObject ironCounter;
    [SerializeField] private Material ironCounterMaterial;
    [SerializeField] private Text ironCounterNumber;
    [SerializeField] private GameObject waterCounter;
    [SerializeField] private Material waterCounterMaterial;
    [SerializeField] private Text waterCounterNumber;
    [SerializeField] private GameObject populationCounter;
    [SerializeField] private Material populationCounterMaterial;
    [SerializeField] private Text populationCounterNumber;

    // Start is called before the first frame update
    void Start()
    {
        GameFlow = GameObject.FindWithTag("GameFlow");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFlow.GetComponent<GameFlow>().food >= 25f)
        {
            foodCounterMaterial.color = Color.green;
        }
        else
        {
            foodCounterMaterial.color = Color.red;
        }
        foodCounter.transform.localScale = new Vector3((float)GameFlow.GetComponent<GameFlow>().food, 30, 1);
        foodCounterNumber.text = GameFlow.GetComponent<GameFlow>().food.ToString();
        if (GameFlow.GetComponent<GameFlow>().wood >= 25f)
        {
            woodCounterMaterial.color = Color.green;
        }
        else
        {
            woodCounterMaterial.color = Color.red;
        }
        woodCounter.transform.localScale = new Vector3((float)GameFlow.GetComponent<GameFlow>().wood, 30, 1);
        woodCounterNumber.text = GameFlow.GetComponent<GameFlow>().wood.ToString();
        if (GameFlow.GetComponent<GameFlow>().iron >= 25f)
        {
            ironCounterMaterial.color = Color.green;
        }
        else
        {
            ironCounterMaterial.color = Color.red;
        }
        ironCounter.transform.localScale = new Vector3((float)GameFlow.GetComponent<GameFlow>().iron, 30, 1);
        ironCounterNumber.text = GameFlow.GetComponent<GameFlow>().iron.ToString();
        if (GameFlow.GetComponent<GameFlow>().water >= 25f)
        {
            waterCounterMaterial.color = Color.green;
        }
        else
        {
            waterCounterMaterial.color = Color.red;
        }
        waterCounter.transform.localScale = new Vector3((float)GameFlow.GetComponent<GameFlow>().water, 30, 1);
        waterCounterNumber.text = GameFlow.GetComponent<GameFlow>().food.ToString();
        if (GameFlow.GetComponent<GameFlow>().population >= 25)
        {
            populationCounterMaterial.color = Color.green;
        }
        else
        {
            populationCounterMaterial.color = Color.red;
        }
        populationCounter.transform.localScale = new Vector3((float)GameFlow.GetComponent<GameFlow>().population, 30, 1);
        populationCounterNumber.text = GameFlow.GetComponent<GameFlow>().population.ToString();
    }
}
