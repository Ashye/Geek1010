using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {




	void OnGUI() {
		GUI.Label(new Rect(0, 0, 100, 10), "test");
		GUI.Label(new Rect(0,Screen.height - 10, 100, 100), "test");
		GUI.Label(new Rect(Screen.width/2,Screen.height/2, 100, 100), "test");
		GUI.Label(new Rect(Screen.width,Screen.height- 100, 100, 100), "test");
	}

	void Awake() {

	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {

	}

	void OnMouseUp() {

	}


	void OnTriggerEnter() {
		print ("sssssssss     enter");
	}
}
