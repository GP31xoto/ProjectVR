﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject ResourceRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeResource;

    void Start(){
        ResourceRef = GameObject.FindGameObjectWithTag("Resource");
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
                    //radius = 10;
                    angle = 180;
                } else {
                    canSeeResource = false;
                }
            } else {
                canSeeResource = false;
            }
        } else if(canSeeResource) {
            canSeeResource = false;
            //radius = 20;
            angle = 90;
        }
    }
}
