using UnityEngine;
using System.Collections;

public class RandomController : MonoBehaviour {

	public GameObject[] scenes;
	public float showtime = 0;//!!!

	private int currentScene = 0;

	// Use this for initialization
	void Start () {
		LoadScene (0);
		StartCoroutine("LoadNext");
	}


	public IEnumerator LoadNext() {
		yield return new WaitForSeconds(showtime);

		gameObject.GetComponent<Animator> ().SetBool ("off", true);
		yield return new WaitForSeconds(0.2f);
		gameObject.GetComponent<Animator> ().SetBool ("off", false);

		currentScene = currentScene >= scenes.Length-1 ? 0:currentScene+1;
		Debug.Log ("L" + scenes.Length + "C" + currentScene);
		LoadScene (currentScene);

		StartCoroutine("LoadNext");
	}

	void OnApplicationFocus(bool focusStatus) {
		Debug.Log ("I got the focus!!");
	}


	void Update() {
		if (Input.GetKey (KeyCode.L)) {
			openBrowser();
		}
	}
	
	// Update is called once per frame
	void LoadScene (int nr) {
		foreach (GameObject scene in scenes) {
			if(scenes[nr] == scene) {
				showtime = scene.GetComponent<SceneInfo>().time;
				scene.SetActive(true);
			} else {
				scene.SetActive(false);
			}
		}
	}

	[ContextMenu ("Open browser")]
	void openBrowser() {
		Application.OpenURL("http://google.com/");
	}
}
