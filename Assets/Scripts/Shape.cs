using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shape : MonoBehaviour {
	//public bool					__________________________;

	private Vector3				oldScale;
	private Vector3				oldPos;
	private Color				shapeColor;
	
	private List<GameObject>	lColliders;
	private List<GameObject>	filledBlocks;
	private bool				blockMoving = false;
	private GameObject			headerToFill;


	void Awake() {
		oldScale = gameObject.transform.localScale;
		lColliders = new List<GameObject> ();
		filledBlocks = new List<GameObject> ();
	}
	// Use this for initialization
	void Start () {
		oldPos = transform.position;
		shapeColor = transform.GetChild (0).renderer.material.color;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		//检查第一位置的块确定图案在网格中的位置，再检查该位置能不能放下
		//第一块的位置是图案的左上角那块
		
		float dis = 20f;
		Transform c1 = transform.GetChild(0);
		Vector3 c1Pos = c1.position;
		
		//print(c1.name);
		//print("c1.position:"+c1Pos);
		
		GameObject b1 = null;
		foreach(GameObject go in lColliders) {
			Vector3 blPos = go.transform.position;
			float tmpDis = Vector3.Distance(c1Pos, blPos);
			if (dis > tmpDis) {
				headerToFill = go;
				dis = tmpDis;
			}
		}

	}


	IEnumerator OnMouseDown() {
		blockMoving = true;
		gameObject.transform.localScale = 1.1f * Vector3.one;


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
		gameObject.transform.localScale = Vector3.one;

		//检查能不能放下，能放下则销毁本对象，并填充网格，不能则图案返回原始位置

		if (CheckSpace()) {
			FillBlock();
			Destroy(gameObject);
		} else {
			transform.position = oldPos;
		}
		lColliders.Clear();
		filledBlocks.Clear();
		//print ("colliders:"+lColliders.Count);
		//print ("filledblock:"+filledBlocks.Count);
	}

	//检查当前位置能不能放下图案
	private bool CheckSpace() {
		if (lColliders.Count > 0 && headerToFill != null) {
			if (true == headerToFill.GetComponent<Block>().empty) {

				filledBlocks.Add(headerToFill);

				Vector3 cblkPos = transform.GetChild(0).position;
				Vector3 bgPos = headerToFill.transform.position;

				//限制最短距离，小于它才能锁定位置
				Vector2 cb = Vector2.zero;
				cb.x = cblkPos.x;
				cb.y = cblkPos.y;
				Vector2 bgp = Vector2.zero;
				bgp.x = bgPos.x;
				bgp.y = bgPos.y;
				if (Vector3.Distance(cb, bgp) >0.7f) {
					return false;
				}

				/*
				 * 检查并筛选图案放置的块
				 * 
				 * 检查方式：同步第一格位置，然后以第一格为基准
				 * 通过各方块中心点到基准方块中心点距离为依据，依次确定该图案填充的背景方块
				 * 
				*/
				Vector3 offset ;
				for (int i=1; i<transform.childCount; i++) {
					offset = transform.GetChild(i).position - cblkPos;
					foreach(GameObject go in lColliders) {
						if (go.transform.position ==  offset+bgPos) {

							if (false == go.GetComponent<Block>().empty) {
								return false;
							}else {
								if (go != headerToFill) {
									filledBlocks.Add(go);
								}
							}
						}
					}
				}
				//print("filledCount:"+filledBlocks.Count);
				if (filledBlocks.Count == transform.childCount) {
					return true;
				}else {
					return false;
				}

			}
		}
		return false;
	}

	private void FillBlock() {
		if (lColliders.Count > 0) {

			foreach(GameObject go in filledBlocks) {
				go.renderer.material.color = shapeColor;

				go.GetComponent<Block>().empty = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (blockMoving) {
			lColliders.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (blockMoving) {
			lColliders.Remove (other.gameObject);
		}
	}
}
