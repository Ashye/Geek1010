using UnityEngine;
using System.Collections;

[SerializeField]
public enum ShapeType {
	ONE,
	TWO_H,
	TWO_V
}



public class MainPlay : MonoBehaviour {
	public GameObject		blockPrefab;
	public GameObject		shapePrefab;

	public bool				______________________;
	public GameObject[,]	blocks;

	//static public MainPlay	PLAY;

	void Awake() {
		//PLAY = this;
		InitGrid ();
	}

	// Use this for initialization
	void Start () {
		GameObject s = MakeShape (ShapeType.ONE);
		s.transform.position = new Vector3 (0f, -2f, 9f);

		GameObject s1 = MakeShape (ShapeType.ONE);
		s1.transform.position = new Vector3 (-2f, -2f, 9f);
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
			}
			pos.y -= 1+gap;
		}
	}

	private GameObject MakeShape(ShapeType st) {
		GameObject shape = null;

		switch (st) {
		case ShapeType.ONE:
			shape = Instantiate(shapePrefab) as GameObject;
			//shape.transform.position = new Vector3(0f, 5f, 10f);
			break;
		}
		shape.tag = "Shape";
		shape.layer = LayerMask.NameToLayer ("Shape");
		return shape;
	}
}
