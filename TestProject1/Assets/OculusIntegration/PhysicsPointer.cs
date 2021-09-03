using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPointer : MonoBehaviour {

    public LayerMask layerMask; // 2 = ignoreraycast layer

    public float defaultLength = 1000.0f;

    public OVRGrabber DistanceGrabber;

    public GameObject Menu;

    public Transform player;

    public float speed = 1.0f;

    //private variables
    private LineRenderer lineRenderer = null;
    private GameObject grabbedObject;
    private GameObject grabbableGOL;
    private GameObject grabbableGOR;

    private GameObject inst;

    private Vector3 originalScale;
    private Vector3 atualScale;
    private Vector3 atualScale2;
    private bool originalScaleDefined = true;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        //layerMask = 1 << 2; // 2 = ignoreraycast layer
        layerMask = ~layerMask;
    }

    //IEnumerator test (Vector3 a, Vector3 b, float time) {
    //    while (true) {
    //        yield return scaleUp( a, b, time);
    //    }
    //}

    //IEnumerator scaleUp(Vector3 a, Vector3 b, float time) {
    //    float i = 0f;
    //    float rate = (1.0f/time)* speed;
    //    while (i < 1.0f) {
    //        i += Time.deltaTime * rate;
    //        transform.localScale = Vector3.Lerp(a,b,i);
    //        yield return null;
    //    }
    //}

    // Update is called once per frame
    private void LateUpdate() {
        UpdateLength();

        //Right Grab
        if (DistanceGrabber.name.Contains("Right")) {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) {
                atualScale = new Vector3();
                originalScale = new Vector3();
                originalScaleDefined = true;
                inst = null;
                Debug.Log("1st");
            }

                

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
                try {
                    if (originalScaleDefined) {
                        originalScale = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                        originalScaleDefined = false;
                    }
                    atualScale = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                    DistanceGrabber.grabbedObject.gameObject.transform.localScale = Vector3.Lerp(atualScale,new Vector3(.02f,.02f,.02f),0.1f);
                    if (DistanceGrabber.grabbedObject.isGrabbed) {
                        Debug.Log(DistanceGrabber.grabbedObject.gameObject);
                        grabbableGOR = DistanceGrabber.grabbedObject.gameObject;
                    }
                } catch (Exception e) {
                    //Debug.Log(e); //Null Exception porque ate o objeto chegar a mao o grabbedobject e nulo
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) {
                Debug.Log(grabbableGOR);
                Destroy(grabbableGOR);
                //GameObject instance = Instantiate(grabbableGOR, CalculateEnd() + Vector3.up, Quaternion.identity);
                inst = Instantiate(grabbableGOR,CalculateEnd() + Vector3.up,Quaternion.identity);

                inst.GetComponent<OVRGrabbable>().enabled = true;
            }
        }

        try { 
            if (Vector3Int.RoundToInt(inst.transform.localScale) == originalScale) {
                atualScale = new Vector3();
                originalScale = new Vector3();
                originalScaleDefined = true;
                inst = null;
                Debug.Log("equal");
            } else {
                atualScale2 = inst.transform.localScale;
                inst.transform.localScale = Vector3.Lerp(atualScale2,originalScale,.1f);
            }
        } catch(Exception e){
            //Debug.Log(e); //Null Exception 
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
