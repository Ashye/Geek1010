using UnityEngine;
using System.Collections;

public class MenuItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp () {
		switch(transform.name) {
		case "hPlay":
		case "goPlay":
			Application.LoadLevel("Play");
			break;

		case "pmHome":
		case "goHome":
			Application.LoadLevel("Home");
			break;
			
		case "pmPlay":
			PlayMenu.pMenu.ShowPlayMenuItems();
			break;

		}
	}
}
