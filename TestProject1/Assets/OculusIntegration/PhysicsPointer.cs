using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPointer : MonoBehaviour{

    private LayerMask layerMask; // 2 = ignoreraycast layer

    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer = null;

    public OVRGrabber DistanceGrabberTest;

    private GameObject grabbedObject;

    private Canvas canvasTest;

    private bool booltest;

    private void Awake(){
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = 1 << 2; // 2 = ignoreraycast layer
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    private void Update() {
        UpdateLength();

        if (OVRInput.GetUp(OVRInput.Button.Four)){
            canvasTest.enabled = !canvasTest.enabled;

        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && booltest) {
            GameObject grabbableGO = DistanceGrabberTest.grabbedObject.gameObject;
            Destroy(DistanceGrabberTest.grabbedObject.gameObject);
            Debug.Log(grabbableGO);
            GameObject instance = Instantiate(grabbableGO,CalculateEnd(),Quaternion.identity);
            instance.GetComponent<OVRGrabbable>().enabled = true;
        }

        if (DistanceGrabberTest.grabbedObject) {
            booltest = true;
            //Debug.Log("True---" + DistanceGrabberTest.grabbedObject);
        } else {
            booltest = false;
            //Debug.Log("False");
        }

    }

    private void UpdateLength() {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    private Vector3 CalculateEnd() {
        RaycastHit hit = RaycastShoot();
        Vector3 endPosition = DefaultEnd(defaultLength);

        if (hit.collider) {
            endPosition = hit.point;
            //Debug.Log(hit.collider.name);
        }
        return endPosition;
    }

    //private RaycastHit CreateForwardRaycast() {
    //    RaycastHit hit;

    //    Ray ray = new Ray(transform.position,transform.forward);

    //    Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    //    Debug.DrawRay(transform.position,transform.forward, Color.green, 10f);
    //    return hit;
    //}

    private Vector3 DefaultEnd(float leng) {
        return transform.position + (transform.forward * leng);
    }

    public RaycastHit RaycastShoot() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position,transform.forward);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)  ) {
            Debug.DrawRay(transform.position,transform.forward * hit.distance, Color.green, 10f);
            //Debug.Log(hit.transform.gameObject);
            grabbedObject = hit.transform.gameObject;
            return hit;
        } else {
            Debug.DrawRay(transform.position,transform.forward * 1000,Color.red,2f);
            return hit; 
        }

    }
}
