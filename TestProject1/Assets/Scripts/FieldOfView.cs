using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject ResourceWaterRef;
    public GameObject ResourceWoodRef;
    public GameObject ResourceFoodRef;
    public GameObject ResourceIronRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeResource;
    public string resourceTypeSeen;

    void Start()
    {
        ResourceFoodRef = GameObject.FindGameObjectWithTag("Food");
        ResourceWoodRef = GameObject.FindGameObjectWithTag("Wood");
        ResourceIronRef = GameObject.FindGameObjectWithTag("Iron");
        ResourceWaterRef = GameObject.FindGameObjectWithTag("Water");
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
                    //radius = 10;
                    angle = 180;
                } else {
                    canSeeResource = false;
                    resourceTypeSeen = "";
                }
            } else {
                canSeeResource = false;
                resourceTypeSeen = "";
            }
        } else if(canSeeResource) {
            canSeeResource = false;
            resourceTypeSeen = "";
            //radius = 20;
            angle = 90;
        }
    }
}
