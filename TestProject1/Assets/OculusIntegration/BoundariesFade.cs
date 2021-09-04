using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesFade : MonoBehaviour{

	public GameObject eyeAnchor;
	private bool simpleFade = true;

    private IEnumerator OnTriggerEnter(Collider other) {
		if (other.tag == "Boundaries" && simpleFade) {
			Debug.Log(simpleFade);
			simpleFade = false;
			fadeIn();
			fadeOut();
			fadeReset();
		}
		yield return null;
	}

	private IEnumerator fadeIn() {
		eyeAnchor.GetComponent<OVRScreenFade>().FadeOut();
		Debug.Log("antes");
		yield return new WaitForSeconds(2);
	}

	private IEnumerator fadeOut() {
		Debug.Log("depois");
		this.transform.position = new Vector3(0,100,0);
		eyeAnchor.GetComponent<OVRScreenFade>().FadeIn();
		yield return new WaitForSeconds(1);
	}


	private IEnumerator fadeReset() {
		yield return new WaitForSeconds(5);
		simpleFade = true;
	}
}
