using UnityEngine;
using System.Collections;

public class InsanityDisco : MonoBehaviour {

	public GameObject ambient;

	void OnEnable() {
		print("script was enabled");
		if (ambient) {
			ambient.GetComponent<AudioSource>().Stop();
		}
	}

	void OnDisable() {
		print("script was removed");
		if (ambient) {
			ambient.GetComponent<AudioSource>().Play();
		}
	}
}
