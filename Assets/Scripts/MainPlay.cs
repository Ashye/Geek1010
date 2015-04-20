using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	private Transform		gridsContainer;
	private float			gridSize;
	private float			gridGap;
	private Vector3			gridLocal;


	private Transform		shapeContainer;
	private Vector3[]		shapePos;
//	private Vector3			shapeInitScale;
	private List<GameObject>	randomShapes;
//	private Hashtable			shapePool;
//	private List<GameObject>	shapePool;
	private Dictionary<int, GameObject>		shapePool;

	//level parameter
	private int 			level;



	void Awake() {
		MPlay = this;



		gridsContainer = transform.GetChild(0);
		shapeContainer = transform.GetChild(1);

		GameObject blk = Instantiate (blockBGPrefab) as GameObject;
		gridSize = blk.renderer.bounds.size.x;
		gridLocal = blk.transform.localScale;
		gridGap = 0.1f;
		Destroy (blk);
		//print (gridSize + "     " + gridGap);

		InitGrid ();


		//random shape pos
		shapePos = new Vector3[3];
		Vector3 p1 = blocks[9,0].transform.position;
		shapePos[0] = p1 + new Vector3(1f, 0f, 0f);
		shapePos[0].z = shapeContainer.transform.position.z;
		shapePos[0].y -= 5*gridSize;

		p1 = blocks[9,9].transform.position;
		shapePos[2] = p1 - new Vector3(1f, 0f, 0f);
		shapePos[2].z = shapeContainer.transform.position.z;
		shapePos[2].y -= 5*gridSize;

		shapePos[1] = (shapePos[0] + shapePos[2])/2;

//		shapeInitScale = new Vector3(0.6f, 0.6f, 1f);


		randomShapes = new List<GameObject>();
//		shapePool = new Hashtable();
//		shapePool = new List<GameObject>();
		shapePool = new Dictionary<int, GameObject>();
	}

	// Use this for initialization
	void Start () {
		level = GameSettings.GS.level;
		RandomShape ();

	}
	 
	// Update is called once per frame
	void FixedUpdate () {
		if (randomShapes.Count <= 0) {
			RandomShape();
		}


//		//android back key
//		if (Input.GetKeyDown(KeyCode.Escape)) {
//			MenuManager.MM.ShowPauseMenu();
//		}
	}


	private void RandomShape() {
		// the number of normal level is 15, the extra are in the hard level
		int maxLen = level == 1? shapePrefabs.Length : 15;
		Random.seed = System.DateTime.Now.Millisecond;
		SoundEffect.SE.MakeShapeShowUpSound();

		//刷新待放置图案
		int idx = -1;
		GameObject go = null;
		for(int i=0; i<shapePos.Length; i++) {
			idx = Random.Range(0, maxLen);
			if (idx >= 15) {
				idx = Random.Range(0, maxLen);
			}
			go = MakeShape(idx);
			go.transform.position = shapePos[i];
			go.GetComponent<Shape>().ReStart();
			randomShapes.Add(go);
		}

//		//check game over
		CheckGameOver();

	}


	private void InitGrid() {
	
		blocks = new GameObject[10, 10];
		Vector3 pos = gridsContainer.transform.position;

		pos.y = 11f;
		//pos.z = -Camera.main.transform.position.z;
		for(int i=0; i<10; i++) {
			for (int j=0; j<10; j++) {
				blocks[i,j] = Instantiate(blockBGPrefab) as GameObject;
				blocks[i,j].tag = "Block";
				blocks[i,j].layer = LayerMask.NameToLayer("Block");
				if (j == 0) {
					pos.x = -5*gridSize - 1f * gridGap;
					//print(pos.x);
				}else {
					pos.x += (gridSize + gridGap);
				}
				//print(pos);
				blocks[i,j].transform.position = pos;
				blocks[i,j].transform.parent = gridsContainer.transform;
				blocks[i,j].name = "Block_bg-"+i+"-"+j;
			}
			pos.y -= (gridSize + gridGap);
		}
	}

	private void PutToPool(GameObject go) {

		go.SetActive(false);

		string id = ((go.name.Substring(5)).Split('('))[0];

		int idx = int.Parse(id);
		if (shapePool.ContainsKey(idx)) {
			Destroy(go);
		}else {
			shapePool.Add(idx, go);
		}

	}

	private GameObject GetFromPool(int idx) {
		GameObject shape = null;

		if ( shapePool.ContainsKey(idx)) {
			shape = shapePool[idx];
			shapePool.Remove(idx);
		}
		return shape;
	}


	private GameObject MakeShape(int idx) {
		if (idx >= shapePrefabs.Length) {
			return null;
		}

		// get shape from shape pool cache
		GameObject shape = GetFromPool(idx);

		if (shape == null) {
//			print("make new one:"+idx);
			shape = Instantiate (shapePrefabs [idx]) as GameObject;
			shape.tag = "Shape";
			shape.layer = LayerMask.NameToLayer ("Shape");
			shape.GetComponent<Shape>().normalSize = gridLocal;
			shape.transform.parent = shapeContainer.transform;
		}else {
//			print("reuse shape:"+idx);
			shape.SetActive(true);
		}

		return shape;
	}

	public void AteOneShape(GameObject go) {
		randomShapes.Remove(go);
		PutToPool(go);
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
		SoundEffect.SE.MakeGameoverSound();
		MenuManager.MM.ShowGameOverMenu();
	}

}
