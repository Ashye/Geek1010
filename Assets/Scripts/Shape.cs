using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shape : MonoBehaviour {
	//public bool					__________________________;

	private Vector3				oldScale;
	private Vector3				oldPos;
	private Color				shapeColor;
	private CapsuleCollider		dragArea;


	//drop blocks
	private List<GameObject>	lColliders;
	private List<GameObject>	filledBlocks;
	private bool				blockMoving = false;
	private GameObject			headerToFill;


	//clear blocks of row or col
	private List<GameObject>	waitToClear;


	//show up animation
	private Vector3				targetPos;
	private bool				isAnimate;
	private float				moveSpeed = 70;



	void Awake() {
		oldScale = gameObject.transform.localScale;
		lColliders = new List<GameObject> ();
		filledBlocks = new List<GameObject> ();
		waitToClear = new List<GameObject> ();

		dragArea = gameObject.GetComponent<CapsuleCollider>();

	}

	// Use this for initialization
	void Start () {

		targetPos = transform.position;
		transform.position = Vector3.one * -10;
		isAnimate = true;


		oldPos = targetPos;
		shapeColor = transform.GetChild (0).renderer.material.color;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		if (isAnimate) {
			if (transform.position != targetPos) {
				transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
			}else {
				isAnimate = false;
			}
		}

		//检查第一位置的块确定图案在网格中的位置，再检查该位置能不能放下
		//第一块的位置是图案的左上角那块
		
		float dis = 20f;
		Transform c1 = transform.GetChild(0);
		Vector3 c1Pos = c1.position;
		
		//print(c1.name);
		//print("c1.position:"+c1Pos);
		
//		GameObject b1 = null;
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

		//禁用 用于拖动区域的碰撞器，避免计算填充块时出现错误
		dragArea.enabled = false;
		gameObject.transform.localScale = 1.01f * Vector3.one;

		//move up some space when drag the shape
		Vector3 tmppos = gameObject.transform.position;
		gameObject.transform.position  = tmppos + new Vector3(0f, 1.5f, 0f);


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
		//恢复可拖动区域状态
		dragArea.enabled = true;
		gameObject.transform.localScale = Vector3.one;
		//检查能不能放下，能放下则销毁本对象，并填充网格，不能则图案返回原始位置

		if (CheckSpace()) {

		//	gameObject.SetActive(false);
			FillBlock();
			Scoreboard.SB.ScoreUp(transform.childCount);

			//chech clear row and col
			CheckBlocksNeedClear();
			ClearRowsACols();
			MainPlay.MPlay.AteOneShape(gameObject);
			Destroy(gameObject);
		} else {
			transform.position = oldPos;
			gameObject.transform.localScale = oldScale;
		}
		headerToFill = null;
		lColliders.Clear();
		filledBlocks.Clear();
		waitToClear.Clear ();

		//print ("colliders:"+lColliders.Count);
		//print ("filledblock:"+filledBlocks.Count);
	}

	//检查当前位置能不能放下图案
	private bool CheckSpace() {
		if (lColliders.Count > 0 && headerToFill != null) {
			if (true == headerToFill.GetComponent<Block> ().empty) {

				filledBlocks.Add (headerToFill);

//				print(headerToFill.name);

				Vector3 cblkPos = transform.GetChild (0).position;
				Vector3 bgPos = headerToFill.transform.position;

				//限制最短距离，小于它才能锁定位置
				Vector2 cb = Vector2.zero;
				cb.x = cblkPos.x;
				cb.y = cblkPos.y;
				Vector2 bgp = Vector2.zero;
				bgp.x = bgPos.x;
				bgp.y = bgPos.y;
				if (Vector3.Distance (cb, bgp) > 0.7f) {
					return false;
				}

				//print("first:"+bgPos+cblkPos);

				/*
				 * 检查并筛选图案放置的块
				 * 
				 * 检查方式：同步第一格位置，然后以第一格为基准
				 * 通过各方块中心点到基准方块中心点距离为依据，依次确定该图案填充的背景方块
				 * 
				*/
				Vector3 offset;
				for (int i=1; i<transform.childCount; i++) {
					offset = transform.GetChild (i).position - cblkPos;

//					print(""+transform.GetChild(i).position+cblkPos);
//					print("offset:"+offset);
					foreach (GameObject go in lColliders) {

//						print(go.transform.position);
//						print(offset+bgPos);
//						print(go.name);
						if (go.transform.position == (offset + bgPos)) {

							if (false == go.GetComponent<Block> ().empty) {
								return false;
							} else {
								if (go != headerToFill) {
									filledBlocks.Add (go);
								}
//								print("22222222");
							}
						}else {
//							print("1111111");
						}
					}
				}

//				print ("filledCount:" + filledBlocks.Count);
				if (filledBlocks.Count == transform.childCount) {
					return true;
				} else {
					return false;
				}
			}
		} else {
//			print("000000000000");
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

	private void CheckBlocksNeedClear() {
		//find the row and col of the shape

		List<GameObject> tmpGos = new List<GameObject> ();
		List<int> checkedRow = new List<int> ();
		List<int> checkedCol = new List<int> ();
		int r, c = 0;
		foreach (GameObject go in filledBlocks) {
			int[] blkRC = go.GetComponent<Block>().rowACol;

			//print(blkRC[0]+"-"+blkRC[1]);

			//check rows
			if (!checkedRow.Contains(blkRC[1])) {
				for(r=0; r<10; r++) {
					if (MainPlay.MPlay.blocks[r, blkRC[1]].GetComponent<Block>().empty) {
						break;
					}else {
						tmpGos.Add(MainPlay.MPlay.blocks[r, blkRC[1]]);
					}
				}
				if (r >= 10) {
					waitToClear.AddRange(tmpGos);
				}
				tmpGos.Clear();
				checkedRow.Add(blkRC[1]);
			}


			//check cols
			if (!checkedCol.Contains(blkRC[0])) {
				for(c=0; c<10; c++) {
					if (MainPlay.MPlay.blocks[blkRC[0], c].GetComponent<Block>().empty) {
						break;
					}else {
						tmpGos.Add(MainPlay.MPlay.blocks[blkRC[0], c]);
					}
				}
				//print(c);
				if (c >= 10) {
					waitToClear.AddRange(tmpGos);
				}
				tmpGos.Clear();
				checkedCol.Add(blkRC[0]);
			}
		}

		//print ("clear count:"+waitToClear.Count);
	}

	private void ClearRowsACols() {
		if (waitToClear.Count > 0) {
			foreach (GameObject go in waitToClear) {
				go.GetComponent<Block> ().empty = true;
				//print(go.name);
			}
			Scoreboard.SB.ScoreUp(waitToClear.Count);
			waitToClear.Clear ();
		} else {
			//print("waitclear :"+waitToClear.Count);
		}
	}

//	void OnTriggerEnter(Collider other) {
//		//print ("sssssssssssssssss");
//		if (blockMoving) {
//			if (!lColliders.Contains(other.gameObject)) {
//				lColliders.Add (other.gameObject);
//			}
//
//		}
//
//		print (lColliders.Count);
//	}

	void OnTriggerStay(Collider other) {
		if (blockMoving) {
			if (!lColliders.Contains(other.gameObject)) {
				lColliders.Add(other.gameObject);
			}
		}

		//print (lColliders.Count);
	}

	void OnTriggerExit(Collider other) {
		if (blockMoving) {
			lColliders.Remove (other.gameObject);
		}
		//print (lColliders.Count);
	}


}
