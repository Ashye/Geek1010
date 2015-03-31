using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {
	public GameObject			pmMenuPrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

	}

	void OnMouseUp() {
		//print("pause game....");
		ShowPlayMenuItems();
	}

	private void ShowPlayMenuItems() {
		GameObject go = Instantiate (pmMenuPrefab) as GameObject;
		go.transform.parent = transform;
	}

}
