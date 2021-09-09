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

    //Right Var
    private GameObject grabbableGOR;
    private GameObject instR;
    private Vector3 originalScaleR;
    private Vector3 atualScaleR;
    private Vector3 atualScale2R;
    private bool originalScaleDefinedR = true;

    //Left Var
    private GameObject grabbableGOL;
    private GameObject instL;
    private Vector3 originalScaleL;
    private Vector3 atualScaleL;
    private Vector3 atualScale2L;
    private bool originalScaleDefinedL = true;

    public OVRInput.Controller cont; 

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

        //Debug.Log("Left: " + OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, cont));
        //Debug.Log("Right: " + OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger, cont));

        //Right Grab
        if (DistanceGrabber.name.Contains("Right")) {

            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger, cont)) {
                atualScaleR = new Vector3();
                originalScaleR = new Vector3();
                originalScaleDefinedR = true;
                instR = null;
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, cont)) {
                try {
                    if (originalScaleDefinedR) {
                        originalScaleR = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                        originalScaleDefinedR = false;
                    }
                    atualScaleR = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                    DistanceGrabber.grabbedObject.gameObject.transform.localScale = Vector3.Lerp(atualScaleR,new Vector3(.02f,.02f,.02f),0.1f);

                    if (DistanceGrabber.grabbedObject.isGrabbed) {
                        Debug.Log(DistanceGrabber.grabbedObject.gameObject);
                        grabbableGOR = DistanceGrabber.grabbedObject.gameObject;

                    }
                } catch (Exception e) {
                    //Debug.Log(e); //Null Exception porque ate o objeto chegar a mao o grabbedobject e nulo
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger,cont)) {
                Destroy(grabbableGOR);
                //GameObject instance = Instantiate(grabbableGOR, CalculateEnd() + Vector3.up, Quaternion.identity);
                instR = Instantiate(grabbableGOR,CalculateEnd() + Vector3.up,Quaternion.identity);
                instR.GetComponent<OVRGrabbable>().enabled = true;
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

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger,cont)) {
                atualScaleL = new Vector3();
                originalScaleL = new Vector3();
                originalScaleDefinedL = true;
                instL = null;
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger,cont)) {
                try {
                    if (originalScaleDefinedL) {
                        originalScaleL = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                        originalScaleDefinedL = false;
                    }
                    atualScaleL = DistanceGrabber.grabbedObject.gameObject.transform.localScale;
                    DistanceGrabber.grabbedObject.gameObject.transform.localScale = Vector3.Lerp(atualScaleL,new Vector3(.02f,.02f,.02f),0.08f);

                    if (DistanceGrabber.grabbedObject.isGrabbed) {
                        Debug.Log(DistanceGrabber.grabbedObject.gameObject);
                        grabbableGOL = DistanceGrabber.grabbedObject.gameObject;
                    }
                } catch (Exception e) {
                    //Debug.Log(e); //Null Exception porque ate o objeto chegar a mao o grabbed object e nulo
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger,cont)) {
                Destroy(grabbableGOL);
                instL = Instantiate(grabbableGOL,CalculateEnd() + Vector3.up,Quaternion.identity);
                instL.GetComponent<OVRGrabbable>().enabled = true;
            }
        }


        try {
            if (Vector3Int.RoundToInt(instR.transform.localScale) == originalScaleR) {
                atualScaleR = new Vector3();
                originalScaleR = new Vector3();
                originalScaleDefinedR = true;
                instR = null;
                Debug.Log("equal");
            } else {
                atualScale2R = instR.transform.localScale;
                instR.transform.localScale = Vector3.Lerp(atualScale2R,originalScaleR,.1f);
            }
        } catch (Exception e) {
            //Debug.Log(e); //Null Exception 
        }

        try {
            if (Vector3Int.RoundToInt(instL.transform.localScale) == originalScaleL) {
                atualScaleL = new Vector3();
                originalScaleL = new Vector3();
                originalScaleDefinedL = true;
                instL = null;
                Debug.Log("equal");
            } else {
                atualScale2L = instL.transform.localScale;
                instL.transform.localScale = Vector3.Lerp(atualScale2L,originalScaleL,.1f);
            }
        } catch (Exception e) {
            //Debug.Log(e); //Null Exception 
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
