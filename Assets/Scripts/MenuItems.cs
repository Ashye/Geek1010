using UnityEngine;
using System.Collections;

public class MenuItems : MonoBehaviour {

//	void OnMouseUp () {
//		switch(transform.name) {
//		case "hPlay":
//		case "goPlay":
//			Application.LoadLevel("Play");
//			break;
//
//		case "pmHome":
//		case "goHome":
//			Application.LoadLevel("Home");
//			break;
//			
//		case "pmPlay":
//			PlayMenu.pMenu.ShowPlayMenuItems();
//			break;
//
//		}
//	}

	public void OnClick() {
		switch(gameObject.name) {
		case "hmPlay":
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

		case "pmPause":
			MenuManager.MM.ShowPauseMenu();
			break;
		}
	}
}
