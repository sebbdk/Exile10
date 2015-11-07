using UnityEngine;
using System.Collections;

public class NextOnClick : MonoBehaviour {

	// Use this for initialization
	void OnClick () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			int i = Application.loadedLevel;
			Application.LoadLevel(i + 1);
		}
	}
}
