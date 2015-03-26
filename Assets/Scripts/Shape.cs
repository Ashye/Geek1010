using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shape : MonoBehaviour {
	//public bool					__________________________;

	private Vector3				oldScale;
	private Vector3				oldPos;
	
	private List<GameObject>	lColliders;
	private bool				blockMoving = false;

	void Awake() {
		oldScale = gameObject.transform.localScale;
		lColliders = new List<GameObject> ();
	}
	// Use this for initialization
	void Start () {
		oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator OnMouseDown() {
		blockMoving = true;
		gameObject.transform.localScale *= 1.1f;


		Vector3 screenSpace = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (
			Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

		while (Input.GetMouseButton(0)) {
			Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			Vector3 Curpos = Camera.main.ScreenToWorldPoint(curScreenSpace)+offset;
			transform.position = Curpos;
			yield return new WaitForFixedUpdate();
		}
	}

	void OnMouseUp() {
		blockMoving = false;
		gameObject.transform.localScale = oldScale;


		//检查当前待填充检查块
		foreach(GameObject go in lColliders) {
			Block script = go.GetComponent<Block>();
			if (script.CoverRate(renderer.bounds) <=0.5f) {
				lColliders.Remove(go);
			}
		}

		print(lColliders.Count);
		//检查能不能放下，能放下则销毁本对象，并填充网格，不能则图案返回原始位置

		if (CheckSpace()) {
			FillBlock();
			lColliders.Clear();
			Destroy(gameObject);
		} else {
			lColliders.Clear();
			transform.position = oldPos;
		}
		print ("colliders:"+lColliders.Count);
	}

	private bool CheckSpace() {
		if (lColliders.Count <= 0) {
			return false;
		} else {
			Block blockScript = null;
			foreach(GameObject go in lColliders) {
				blockScript = go.GetComponent<Block>();
				if (blockScript != null && blockScript.empty) {
					continue;
				}else {
					return false;
				}
			}
			return true;
		}
	}

	private void FillBlock() {
		if (lColliders.Count > 0) {
			foreach(GameObject go in lColliders) {
				go.renderer.material.color = gameObject.renderer.material.color;
				go.GetComponent<Block>().empty = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//print ("shape trigger enter");
		if (blockMoving) {
			lColliders.Add (other.gameObject);
		}
	}

//	void OnTriggerStay(Collider other) {
//		print ("trigger stay");
//
//		//if 
//	}

	void OnTriggerExit(Collider other) {
		//print ("shape trigger exit");
		if (blockMoving) {
			lColliders.Remove (other.gameObject);
		}
		//print ();
	}
}
