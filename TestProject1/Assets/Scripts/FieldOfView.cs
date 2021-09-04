using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject[] ResourceWaterRef;
    public GameObject[] ResourceWoodRef;
    public GameObject[] ResourceFoodRef;
    public GameObject[] ResourceIronRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeResource;
    public string resourceTypeSeen;
    public int resourceTypeIndex;

    void Start()
    {
        ResourceFoodRef = GameObject.FindGameObjectsWithTag("Food");
        ResourceWoodRef = GameObject.FindGameObjectsWithTag("Wood");
        ResourceIronRef = GameObject.FindGameObjectsWithTag("Iron");
        ResourceWaterRef = GameObject.FindGameObjectsWithTag("Water");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck(){
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward,directionToTarget) < angle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position,target.position);

                if (!Physics.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionMask)) {
                    canSeeResource = true;
                    resourceTypeSeen = target.gameObject.tag;
                    if (resourceTypeSeen == "Food")
                    {
                        for(int i = 0; i < ResourceFoodRef.Length; i++)
                        {
                            if(ResourceFoodRef[i] == target.gameObject)
                            {
                                resourceTypeIndex = i;
                                break;
                            }
                        }
                    }else if (resourceTypeSeen == "Wood")
                    {
                        for (int i = 0; i < ResourceWoodRef.Length; i++)
                        {
                            if (ResourceWoodRef[i] == target.gameObject)
                            {
                                resourceTypeIndex = i;
                                break;
                            }
                        }
                    }
                    else if (resourceTypeSeen == "Iron")
                    {
                        for (int i = 0; i < ResourceIronRef.Length; i++)
                        {
                            if (ResourceIronRef[i] == target.gameObject)
                            {
                                resourceTypeIndex = i;
                                break;
                            }
                        }
                    }
                    else if (resourceTypeSeen == "Water")
                    {
                        for (int i = 0; i < ResourceWaterRef.Length; i++)
                        {
                            if (ResourceWaterRef[i] == target.gameObject)
                            {
                                resourceTypeIndex = i;
                                break;
                            }
                        }
                    }
                    //radius = 10;
                    angle = 180;
                } else {
                    canSeeResource = false;
                    resourceTypeSeen = "";
                    resourceTypeIndex = -1;
                }
            } else {
                canSeeResource = false;
                resourceTypeSeen = "";
                resourceTypeIndex = -1;
            }
        } else if(canSeeResource) {
            canSeeResource = false;
            resourceTypeSeen = "";
            resourceTypeIndex = -1;
            //radius = 20;
            angle = 90;
        }
    }
}
