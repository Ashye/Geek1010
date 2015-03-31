using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

	}

	void OnMouseUp() {
		print("pause game....");
		ShowPlayMenuItems();
	}

	private void ShowPlayMenuItems() {
		GameObject go = new GameObject();
		go.layer = LayerMask.NameToLayer("Normal");
		go.transform.localScale = new Vector3(11f, 20f, transform.position.z -0.5f);
		go.AddComponent<Rigidbody>();
		go.AddComponent<PlayMenuItems>();
		go.AddComponent<BoxCollider>();
		go.rigidbody.useGravity = false;
		go.transform.position = new Vector3(-.4f, 5.5f, transform.position.z-1);
		go.transform.parent = transform.parent;
	}
}
