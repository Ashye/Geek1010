using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {


	static Block		B;
	private bool			_empty = true;



	void Awake() {
		B = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float CoverRate(Bounds cover) {
		print ("=================");
		print (transform.position);
		print (renderer.bounds.center);
		print (renderer.bounds.size);
		print ("----------");
		print (cover.center);
		print (cover.size);
		Vector3 point1 = Vector3.zero;
		Vector3 point2 = Vector3.zero;

		point1.x = transform.position.x - renderer.bounds.size.x / 2;
		point1.y = cover.center.y + cover.size.y / 2;

		point2.y = transform.position.y - renderer.bounds.size.y / 2;
		point2.x = cover.center.x + cover.size.x / 2;

		Vector3 point = point1 - point2;

		float area = Mathf.Abs (point.x) * Mathf.Abs (point.y);
		print ("cover area:"+area);

		return area;
	}

	public bool empty {
		get {
			return _empty;
		}
		set {
			_empty = value;
		}
	}

//	void OnTriggerEnter(Collider other) {
//		print ("trigger enter");
//	}
//	
//	void OnTriggerStay(Collider other) {
//		print ("trigger stay");
//		
//		//if 
//	}
//	
//	void OnTriggerExit(Collider other) {
//		print ("trigger exit");
//	}
}
