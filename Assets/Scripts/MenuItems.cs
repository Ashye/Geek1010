﻿using UnityEngine;
using System.Collections;

public class MenuItems : MonoBehaviour {


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
			MenuManager.MM.HidePauseMenu();
			break;

		case "pmPause":
			MenuManager.MM.ShowPauseMenu();
			break;
		}
	}
}
