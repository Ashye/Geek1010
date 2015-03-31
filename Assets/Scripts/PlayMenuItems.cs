using UnityEngine;
using System.Collections;

public class PlayMenuItems : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {

		switch(gameObject.name) {
		case "pmHome":
			Application.LoadLevel("Home");
			break;

		case "pmPlay":
			Destroy(transform.parent.gameObject);
			break;
		}
	}
}
