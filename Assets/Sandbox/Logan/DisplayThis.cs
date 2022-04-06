using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayThis : MonoBehaviour {

	public float secs = 1f;

	void OnEnable() {
		StartCoroutine(DeactivateAfter());
	}

	public void LongerSecs(){
		secs = 2f;
	}

	IEnumerator DeactivateAfter() {
		yield return new WaitForSeconds(secs);
		gameObject.SetActive(false);
		secs = 1f;
	}


}
