using UnityEngine;
using System.Collections;

[SerializeField]
public enum ShapeType {
	SHAPE_0,
	SHAPE_1,
	SHAPE_2,
	SHAPE_3,
	SHAPE_4,
	SHAPE_5,
	SHAPE_6
}



public class MainPlay : MonoBehaviour {
	public GameObject		blockPrefab;
	public GameObject[]		shapePrefabs;

	public bool				______________________;
	public GameObject[,]	blocks;

	//static public MainPlay	PLAY;

	void Awake() {
		//PLAY = this;
		InitGrid ();
	}

	// Use this for initialization
	void Start () {
		GameObject s = MakeShape (ShapeType.SHAPE_3);
		s.transform.position = new Vector3 (0f, -2f, 9f);

		GameObject s1 = MakeShape (ShapeType.SHAPE_5);
		s1.transform.position = new Vector3 (-4.5f, -2f, 9f);

		GameObject s2 = MakeShape (ShapeType.SHAPE_6);
		s2.transform.position = new Vector3 (4.5f, -2f, 9f);
	}
	 
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomShape() {
		//刷新待放置图案
	}

//	public bool CheckCanDrop() {
//		return true;
//	}

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
				blocks[i,j].name = "Block_bg"+i+"-"+j;
			}
			pos.y -= 1+gap;
		}

//		print (blocks[0,0].transform.position);
//		print (blocks[1,0].transform.position);
//		print (blocks[2,0].transform.position);
	}

//	private GameObject ShapeInit(GameObject shape) {
//		shape.AddComponent<BoxCollider>();
//		shape.collider.isTrigger = true;
//		shape.AddComponent<Shape>();
//		shape.AddComponent<Rigidbody>();
//		shape.rigidbody.useGravity = false;
//
//		GameObject blk = Instantiate(shapePrefabs[0]) as GameObject;
//		blk.transform.parent = shape.transform;
//		return shape;
//	}
//
//	/* edge: 0.left		1.top	2.right		3.bottom */
//	private GameObject ShapeAddBlk(GameObject shape, int edge) {
//		GameObject blk = Instantiate (blockPrefab[0]) as GameObject;
//		return shape;
//	}

	private GameObject MakeShape(ShapeType st) {

		int idx = st.GetHashCode ();
		GameObject shape = new GameObject("shape");
		//print (st.GetHashCode());
		shape = Instantiate (shapePrefabs [idx]) as GameObject;
		shape.tag = "Shape";
		shape.layer = LayerMask.NameToLayer ("Shape");
		return shape;

	}
}
