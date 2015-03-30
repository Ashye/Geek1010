using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {




	void Awake() {
		foreach(BoxCollider bc in gameObject.GetComponents<BoxCollider>()) {
			print(bc.name);
		}
	}

	// Use this for initialization
	void Start () {

		Vector3 size = transform.renderer.bounds.size;
		Vector3 center = transform.renderer.bounds.center;
		Vector3 pos = transform.position;
//
//		print (pos);
//		print (center);
//		print (size);
//
//
//		print ("------world 2 screen-------");
//		print (Camera.main.WorldToScreenPoint(pos));
//		print (Camera.main.WorldToScreenPoint(center));
//		print (Camera.main.WorldToScreenPoint(size));

		transform.position = Camera.main.ScreenToWorldPoint (new Vector3(100f, 10f, 10f));

//		print (""+Screen.width+" - "+Screen.height);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		print("mouse down event");
		gameObject.GetComponent<Collider>().enabled = false;

//		gameObject.getcomponent
	}

	void OnMouseUp() {
		gameObject.GetComponent<Collider>().enabled = true;
	}


	void OnTriggerEnter() {
		print ("sssssssss     enter");
	}
}
