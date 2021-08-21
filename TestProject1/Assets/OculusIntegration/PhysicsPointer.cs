using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPointer : MonoBehaviour{

    private LayerMask layerMask; // 2 = ignoreraycast layer

    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer = null;

    public OVRGrabber DistanceGrabber;

    private GameObject grabbedObject;

    private Canvas canvasTest;

    private bool boolLeftHand;
    private bool boolRightHand;
    private bool booltest;

    private GameObject grabbableGO;

    private void Awake(){
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = 1 << 2; // 2 = ignoreraycast layer
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    private void Update() {
        UpdateLength();

        //if (OVRInput.GetUp(OVRInput.Button.Four)) {
        //    canvasTest.enabled = !canvasTest.enabled;
        //}

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && booltest) {
            Destroy(grabbableGO);
            GameObject instance = Instantiate(grabbableGO,CalculateEnd(),Quaternion.identity);
            instance.GetComponent<OVRGrabbable>().enabled = true;
        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) && booltest) {
            GameObject grabbableGO = DistanceGrabber.grabbedObject.gameObject;
            Destroy(DistanceGrabber.grabbedObject.gameObject);
            Debug.Log(grabbableGO);
            GameObject instance = Instantiate(grabbableGO,CalculateEnd(),Quaternion.identity);
            instance.GetComponent<OVRGrabbable>().enabled = true;
        }

        //if (DistanceGrabber.name.Contains("Right")) {
        //    if (DistanceGrabber.grabbedObject) {
        //        boolRightHand = true;
        //        //Debug.Log("Right Grab");
        //    } else {
        //        boolRightHand = false;
        //        //Debug.Log("Right Not Grab");
        //    }
        //}

        //if (DistanceGrabber.name.Contains("Left")) {
        //    if (DistanceGrabber.grabbedObject) {
        //        boolLeftHand = true;
        //        //Debug.Log("Left Grab");
        //    } else {
        //        boolLeftHand = false;
        //    }
        //}

        if (DistanceGrabber.grabbedObject) {
            Debug.Log("true");
            booltest = true;
            grabbableGO = DistanceGrabber.grabbedObject.gameObject;
        } else {
            Debug.Log("False");

            booltest = false;
            grabbableGO = null;
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
