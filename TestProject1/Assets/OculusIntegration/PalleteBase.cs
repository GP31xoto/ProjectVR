using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalleteBase : MonoBehaviour{

    public GameObject pallete;
    public GameObject basedObj;
    private Vector3 scale;
    private GameObject i;

    private void Start() {
        scale = basedObj.transform.localScale;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log(other);
        if (other.gameObject.tag.Contains("PalleteGrabbable")) {
            Debug.Log("BaseCollide2 - " + other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(other);
        if (other.gameObject.tag.Contains("Pallete")) {
            Debug.Log("Bye- " + other.gameObject);
            i = Instantiate(basedObj,Vector3.one, Quaternion.identity);
            i.transform.SetParent(pallete.transform);
            i.transform.localScale = Vector3.one;
            i.transform.localRotation = new Quaternion(0,0,0,0);
            i.transform.position = new Vector3(5,0.15f,5);
        }
    }

    private void Update() {
        i.transform.localRotation = new Quaternion(0,0,0,0);
        Debug.Log(i);
        i.transform.position = new Vector3(5,0.15f,5);
    }

}
