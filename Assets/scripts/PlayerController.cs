using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject head;

	public GameObject front;
	public GameObject back;

	public bool dance;

	public GameObject[] animators;

	// Update is called once per frame
	void Update () {
		handleWalking ();
	}

	public void reload() {
		Application.LoadLevel(Application.loadedLevel);
	}

	void handleWalking() {
		Vector2 dir = new Vector2 ();
		if (Input.GetKey ("w")) {
			dir.y += 1;
		}
		if (Input.GetKey ("s")) {
			dir.y -= 1;
		}
		if (Input.GetKey ("a")) {
			dir.x -= 1;
		}
		if (Input.GetKey ("d")) {
			dir.x += 1;
		}

		if (dir.y > 0) {
			back.SetActive (true);
			front.SetActive (false);
		} else if(dir.y < 0 || dir.x != 0) {
			front.SetActive(true);
			back.SetActive (false);
		}

		foreach(GameObject obj in animators) {
			obj.GetComponent<Animator>().SetBool("dance", true);

			if(dir.y != 0 || dir.x != 0) {
				Debug.Log ("Moving!!");
				obj.GetComponent<Animator>().SetBool("moving", true);
			} else {
				obj.GetComponent<Animator>().SetBool("moving", false);
			}
		}

		if (dir.x != 0 && head) {
			head.transform.localScale = new Vector2(-dir.x, 1);
		}
		
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		body.velocity = dir * 2;
		//body.transform.position = new Vector3 (body.transform.position.x, body.transform.position.y, body.transform.position.y-0.4f);
	}
}
