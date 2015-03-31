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


/*
 * 
 * gridSize: 0.9
 * girdGap: 0.1
 * offset: -0.4 (Camera)
 * 
 */
public class MainPlay : MonoBehaviour {
	static public MainPlay	MPlay;

	public GameObject		blockBGPrefab;
	public GameObject[]		shapePrefabs;

	public bool				______________________;
	public GameObject[,]	blocks;
	private float			gridSize;
	private float			gridGap;
	private Vector3			gridLocal;


	private Vector3[]		shapePos;
	private List<GameObject>	randomShapes;	





	void Awake() {
		MPlay = this;

		GameObject blk = Instantiate (blockBGPrefab) as GameObject;
		gridSize = blk.renderer.bounds.size.x;
		gridLocal = blk.transform.localScale;
		gridGap = 0.12f;
		Destroy (blk);
		//print (gridSize + "     " + gridGap);

		InitGrid ();


		//random shape pos
		shapePos = new Vector3[3];
		Vector3 p1 = blocks[9,0].transform.position;
		shapePos[0] = p1 + new Vector3(1f, 0f, 0f);
		shapePos[0].z = 9f;
		shapePos[0].y -= 5*gridSize;

		p1 = blocks[9,9].transform.position;
		shapePos[2] = p1 - new Vector3(1f, 0f, 0f);
		shapePos[2].z = 9f;
		shapePos[2].y -= 5*gridSize;

		shapePos[1] = (shapePos[0] + shapePos[2])/2;


		randomShapes = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		RandomShape ();
	}
	 
	// Update is called once per frame
	void FixedUpdate () {
		if (randomShapes.Count <= 0) {
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
			randomShapes.Add(go);
		}

		//check game over
		CheckGameOver();

	}


	private void InitGrid() {

		GameObject grid = new GameObject ("blockGrid");
		blocks = new GameObject[10, 10];
		Vector3 pos = Vector3.zero;

		pos.y = 12f;
		pos.z = -Camera.main.transform.position.z;
		for(int i=0; i<10; i++) {
			for (int j=0; j<10; j++) {
				blocks[i,j] = Instantiate(blockBGPrefab) as GameObject;
				blocks[i,j].tag = "Block";
				blocks[i,j].layer = LayerMask.NameToLayer("Block");
				if (j == 0) {
					pos.x = -5*gridSize - 4.5f * gridGap;
					//print(pos.x);
				}else {
					pos.x += gridSize + gridGap;
				}
				//print(pos);
				blocks[i,j].transform.position = pos;
				blocks[i,j].transform.parent = grid.transform;
//				blocks[i,j].transform.localScale = Vector3.one * gridSize;
				blocks[i,j].name = "Block_bg-"+i+"-"+j;
			}
			pos.y -= gridSize + gridGap;
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
		shape.GetComponent<Shape>().normalSize = gridLocal;
		return shape;
	}

	public void AteOneShape(GameObject go) {
		randomShapes.Remove(go);
		CheckGameOver();
	}


	public bool ShapeCanDrop(GameObject shape) {

//		print("test shape:"+shape.name+ "   blocks: "+shape.transform.childCount);
		bool canDrop = false;
		Vector3 local = shape.transform.localScale;
		shape.transform.localScale = gridLocal;

		//依次把图案放在网格中尝试放下
		Vector3 shape0Pos = shape.transform.GetChild(0).position;
		Vector3 offset;
		Vector3 grid0Pos;

		foreach(GameObject header in blocks) {

			if (false == header.GetComponent<Block>().empty) {
				continue;
			}
			grid0Pos = header.transform.position;
			int count = 0;
			for(int m=0; m<shape.transform.childCount; m++) {
				offset = shape.transform.GetChild(m).position - shape0Pos;

				foreach(GameObject grid in blocks) {

					if (Vector3.Distance(grid.transform.position, (grid0Pos+offset)) < 0.06f) {
//					if (grid.transform.position == (grid0Pos + offset)) {
						if (false == grid.GetComponent<Block>().empty) {
							break;
						}else {
							count++;
						}
					}
				}

			}
		//	print("free bloocks:"+count);
			if (count == shape.transform.childCount) {
				canDrop = true;
				break;
			}
		}

		shape.transform.localScale = local;
		return canDrop;
	}

	private void CheckGameOver() {
		if (randomShapes.Count >0) {
			bool isOver = true;

			//check gameover flow
			foreach(GameObject go in randomShapes) {

				bool candrop = ShapeCanDrop(go);
//				print(go.name+"-"+candrop);
				if (candrop) {
					isOver = false;
					break;
				}
			}


//			print("isOver:"+isOver);
			if (isOver) {
				ShowGameover();
			}
		}
	}


	//gameover view
	private void ShowGameover () {
		GameObject go = new GameObject();
		go.transform.localScale = Vector3.one * 15;
		go.AddComponent<Rigidbody>();
		go.AddComponent<GameOver>();
		go.AddComponent<BoxCollider>();
		go.rigidbody.useGravity = false;
		go.transform.position = Vector3.zero;
		go.transform.parent = gameObject.transform;
	}
}
