using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	static public MenuManager		MM;


	public GameObject				pmMenu;
	public GameObject				goMenu;


	private GameObject				menu;



	void Awake() {
		MM = this;
	}

	public void ShowPauseMenu() {
		menu = pmMenu;
		menu.SetActive(true);
	}

	public void HidePauseMenu() {
		menu.SetActive(false);
	}

	public void ShowGameOverMenu() {
		menu = goMenu;
		menu.SetActive(true);
	}

//	public void ToggleMuteSound() {
//		SoundEffect.SE.MuteSound(!SoundEffect.SE.mute);

		//fresh sound menu materials

//	}
}
