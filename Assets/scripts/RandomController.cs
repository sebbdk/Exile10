using UnityEngine;
using System.Collections;

public class RandomController : MonoBehaviour {

	public GameObject[] scenes;
	public float showtime = 0;//!!!

	private int currentScene = 0;
	private int gotoOnFocus = 0;
	private SceneInfo lastInfo = null;

	private string[] websites = {
		"http://iloveyoulikeafatladylovesapples.com/",
		"http://www.sadforjapan.com/",
		"http://salmonofcapistrano.com/",
		"http://secretsfornicotine.com/",
		"http://eelslap.com/",
		"http://www.fallingfalling.com/",
		"http://www.papertoilet.com/",
		"http://gifdanceparty.giphy.com/",
		"http://www.staggeringbeauty.com/",
		"http://www.pointerpointer.com/",
		"http://www.rrrgggbbb.com/",
		"http://www.leduchamp.com/",
		"http://www.muchbetterthanthis.com/"
	};

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

		loadNextNow ();
		
		if (!lastInfo.website) {
			StartCoroutine ("LoadNext");
		}
	}

	void loadNextNow() {
		currentScene = currentScene >= scenes.Length-1 ? 0:currentScene+1;
		Debug.Log ("L" + scenes.Length + "C" + currentScene);
		lastInfo = scenes[currentScene].GetComponent<SceneInfo>();
		LoadScene (currentScene);
	}

	void OnApplicationFocus(bool focusStatus) {
		if (lastInfo != null && lastInfo.website) {
			Debug.Log ("Open site!");
			loadNextNow();
			StartCoroutine("LoadNext");
		}
	}


	void Update() {
		if (Input.GetKey (KeyCode.L)) {
			openBrowser();
		}
	}
	
	// Update is called once per frame
	void LoadScene (int nr) {
		foreach (GameObject scene in scenes) {
			SceneInfo info = scene.GetComponent<SceneInfo>();

			if(scenes[nr] == scene) {
				if(!scene.activeSelf) {

					if(info.website == true) {
						string site = websites[ Mathf.RoundToInt( Random.Range(0, websites.Length-1) ) ];
						Application.OpenURL(site);
					}

					showtime = info.time;
					scene.SetActive(true);
				}
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
