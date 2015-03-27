using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeField]
public enum ShapeType {
	SHAPE_0,
	SHAPE_1,
	SHAPE_2,
	SHAPE_3,
	SHAPE_4,
	SHAPE_5,
	SHAPE_6,
	SHAPE_7,
	SHAPE_8,
	SHAPE_9,
	SHAPE_10,
	SHAPE_11,
	SHAPE_12,
	SHAPE_13,
	SHAPE_14,
	SHAPE_15,
	SHAPE_16,
	SHAPE_17,
	SHAPE_18
}



public class MainPlay : MonoBehaviour {
	static public MainPlay	MPlay;

	public GameObject		blockPrefab;
	public GameObject[]		shapePrefabs;

	public bool				______________________;
	public GameObject[,]	blocks;


	private Vector3[]		shapePos;
	private int 			freeShapeCnt;

	//clear block
	//private List<GameObject>	waitToClear;
	//private Color				gridColor;



	void Awake() {
		MPlay = this;
		InitGrid ();
		shapePos = new Vector3[3];
		shapePos[0] = new Vector3 (0f, -3f, 9f);
		shapePos[1] = new Vector3 (-4.5f, -3f, 9f);
		shapePos[2] = new Vector3 (4.5f, -3f, 9f);
		freeShapeCnt = 0;
		//waitToClear = new List<GameObject> ();

	}

	// Use this for initialization
	void Start () {
		RandomShape ();
	}
	 
	// Update is called once per frame
	void FixedUpdate () {
		if (freeShapeCnt <= 0) {
			RandomShape();
		}
	}



	private void RandomShape() {
		//刷新待放置图案
		int idx = -1;
		GameObject go = null;
		for(int i=0; i<shapePos.Length; i++) {
			idx = Random.Range(0, shapePrefabs.Length);
			go = MakeShape(idx);
			go.transform.position = shapePos[i];
		}
		freeShapeCnt = shapePos.Length;

	}


	private void InitGrid() {

		GameObject grid = new GameObject ("blockGrid");
		blocks = new GameObject[10, 10];
		Vector3 pos = Vector3.zero;
		float gap = 0.2f;
		pos.x = 5.9f;
		pos.y = 12f;
		pos.z = -Camera.main.transform.position.z;
		for(int i=0; i<10; i++) {
			for (int j=0; j<10; j++) {
				blocks[i,j] = Instantiate(blockPrefab) as GameObject;
				blocks[i,j].tag = "Block";
				blocks[i,j].layer = LayerMask.NameToLayer("Block");
				if (j == 0) {
					pos.x = -5.5f;
				}else {
					pos.x += 1.2f;
				}
				blocks[i,j].transform.position = pos;
				blocks[i,j].transform.parent = grid.transform;
				blocks[i,j].name = "Block_bg-"+i+"-"+j;
			}
			pos.y -= 1+gap;
		}
	}


	private GameObject MakeShape(ShapeType st) {
		return (MakeShape (st.GetHashCode ()));
	}

	private GameObject MakeShape(int idx) {
		if (idx >= shapePrefabs.Length) {
			return null;
		}
		GameObject shape = null;
		
		shape = Instantiate (shapePrefabs [idx]) as GameObject;
		shape.tag = "Shape";
		shape.layer = LayerMask.NameToLayer ("Shape");
		return shape;
	}

	public void AteOneShape() {
		freeShapeCnt -= 1;
	}
}
