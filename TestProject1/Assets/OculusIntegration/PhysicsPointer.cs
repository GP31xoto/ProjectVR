using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPointer : MonoBehaviour{

    public LayerMask layerMask; // 2 = ignoreraycast layer

    public float defaultLength = 1000.0f;

    private LineRenderer lineRenderer = null;

    public OVRGrabber DistanceGrabber;

    private GameObject grabbedObject;

    private GameObject grabbableGOL;
    private GameObject grabbableGOR;

    public GameObject Menu;

    public Transform player;

    private void Awake(){
        lineRenderer = GetComponent<LineRenderer>();
        //layerMask = 1 << 2; // 2 = ignoreraycast layer
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    private void LateUpdate() {
        UpdateLength();

        //Right Grab
        if (DistanceGrabber.name.Contains("Right")) {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
                try { 
                    if (DistanceGrabber.grabbedObject.isGrabbed) { 
                        Debug.Log(DistanceGrabber.grabbedObject.gameObject);
                        grabbableGOR = DistanceGrabber.grabbedObject.gameObject;
                    }
                } catch(Exception e){
                    //Debug.Log(e); //Null Exception porque ate o objeto chegar a mao o grabbedobject e nulo
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) {
                Debug.Log(grabbableGOR);
                Destroy(grabbableGOR);
                GameObject instance = Instantiate(grabbableGOR,CalculateEnd(),Quaternion.identity);
                instance.GetComponent<OVRGrabbable>().enabled = true;
            }
        }
        
        //Left Grab
        if (DistanceGrabber.name.Contains("Left")) {

            if (OVRInput.GetDown(OVRInput.RawButton.X)) {
                //Debug.Log("Pressed X");
                Menu.SetActive(!Menu.activeSelf);
            } else if (OVRInput.GetDown(OVRInput.RawButton.Y)) {
                //Debug.Log("Pressed Y");
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
                try {
                    if (DistanceGrabber.grabbedObject.isGrabbed) {
                        Debug.Log(DistanceGrabber.grabbedObject.gameObject);
                        grabbableGOL = DistanceGrabber.grabbedObject.gameObject;
                    }
                } catch (Exception e) {
                    //Debug.Log(e); //Null Exception porque ate o objeto chegar a mao o grabbed object e nulo
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)) {
                Debug.Log(grabbableGOL);
                Destroy(grabbableGOL);
                GameObject instance = Instantiate(grabbableGOL,CalculateEnd(),Quaternion.identity);
                instance.GetComponent<OVRGrabbable>().enabled = true;
            }
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
