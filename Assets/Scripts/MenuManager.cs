using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	static public MenuManager		MM;


	public GameObject				pmMenu;
	public GameObject				goMenu;


	private GameObject				menu = null;



	void Awake() {
		MM = this;
	}

	void FixedUpdate() {
		//android back key

		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (menu == null) {
				ShowPauseMenu();
			}else if (menu == pmMenu) {
				HidePauseMenu();
			}else if (menu == goMenu) {
				Application.LoadLevel("Home");
//			}else {
//				Application.LoadLevel("Home");
			}
		}else if (Input.GetKeyUp(KeyCode.Menu)) {
			if (menu == null) {
				ShowPauseMenu();
			}
		}
	}

	public void ShowPauseMenu() {
		menu = pmMenu;
		menu.SetActive(true);
	}

	public void HidePauseMenu() {
		menu.SetActive(false);
		menu = null;
	}

	public void ShowGameOverMenu() {
		menu = goMenu;
		menu.SetActive(true);
	}

}
