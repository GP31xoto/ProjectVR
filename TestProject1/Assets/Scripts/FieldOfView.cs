using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public Vector3 resourcePos;

    public GameObject[] ResourceRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeResource;
    public string resourceTypeSeen;
    public int resourceTypeIndex;

    void Start()
    {
        ResourceRef = GameObject.FindGameObjectsWithTag("Resource");
        resourcePos = gameObject.transform.position;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    public void UpdateResourceRef()
    {
        ResourceRef = GameObject.FindGameObjectsWithTag("Resource");
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
                    for (int i = 0; i < ResourceRef.Length; i++)
                    {
                        if (ResourceRef[i] == target.gameObject)
                        {
                            resourceTypeIndex = i;
                            break;
                        }
                    }
                    //radius = 10;
                    angle = 180;
                    resourcePos = target.position;
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
