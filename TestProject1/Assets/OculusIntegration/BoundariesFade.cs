using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesFade : MonoBehaviour{

	public GameObject eyeAnchor, player, hand;
	public BoxCollider bc;

    private void Update() {
		this.transform.position = hand.transform.position;
		this.transform.localScale = Vector3.one;
		bc.enabled = true;
		bc.center = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("enter");
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("stay");
		Debug.Log(other);

		if (other.gameObject.tag == "Boundaries") {

			StartCoroutine(fadeIn());
			StartCoroutine(fadeOut());
		}
    }

	private IEnumerator fadeIn() {
		eyeAnchor.GetComponent<OVRScreenFade>().FadeOut();
		Debug.Log("antes");
		yield return new WaitForSeconds(2);
	}

	private IEnumerator fadeOut() {
		Debug.Log("depois");
		player.transform.position = new Vector3(0,100,0);
		eyeAnchor.GetComponent<OVRScreenFade>().FadeIn();
		yield return new WaitForSeconds(1);
	}
}
