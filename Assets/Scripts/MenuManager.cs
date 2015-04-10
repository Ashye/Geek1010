using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	static public MenuManager		MM;


	public GameObject				pmPrefab;


	private GameObject				menu;


	void Awake() {
		MM = this;
	}

	public void ShowPauseMenu() {
		menu = Instantiate(pmPrefab) as GameObject;
		menu.transform.SetParent(transform.parent);
		menu.transform.position = transform.parent.position;
		menu.transform.localScale = Vector3.one;
	}

	public void HidePauseMenu() {

	}

	public void ShowGameOverMenu() {

	}
}
