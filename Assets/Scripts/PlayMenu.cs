using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {
	public GameObject			pmMenuPrefab;
	static public PlayMenu		pMenu;

	private bool				isShowing = false;

	void Awake() {
		pMenu = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

	}

	void OnMouseUp() {
		ShowPlayMenuItems();
	}

	public void ShowPlayMenuItems() {
		if (false == isShowing) {
			GameObject go = Instantiate (pmMenuPrefab) as GameObject;
			go.transform.parent = transform;
			isShowing = true;
		}else {
			Destroy(transform.GetChild(0).gameObject);
			isShowing = false;
		}
	}

}
