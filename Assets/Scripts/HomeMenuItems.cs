using UnityEngine;
using System.Collections;

public class HomeMenuItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void OnMouseUp () {
		switch(transform.name) {
		case "hPlay":
			Application.LoadLevel("Play");
			break;
		}
	}
}
