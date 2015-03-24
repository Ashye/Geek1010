using UnityEngine;
using System.Collections;

public class MainPlay : MonoBehaviour {

	public GameObject			blockPrefab;

	public bool 				______________________;
	private float				blackSize;
	private float				screenSize;

	//public GameObject
	public GameObject[,]	blocks;

	void Awake() {
		initGrid ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void initGrid() {
		blocks = new GameObject[10, 10];
		Vector3 pos = new Vector3(0.5f, 11f, 0f);
		for (int i=0; i<10; i++) {
			for(int j=0; j<10; j++) {
				blocks[i,j] = Instantiate(blockPrefab) as GameObject;
				if (j == 0) {
					pos.x = 0.5f - 1.15f;
				}else if (j <5) {
					//pos.x = -1 * j - j * 0.1f;
					pos.x += -1.15f;
				}else if (j == 5) {
					pos.x = 0.5f;
				}else {
					//pos.x = 1 * (j-4) + (j-4) * 0.1f;
					pos.x += 1.15f;
				}
				blocks[i,j].transform.position = pos;
				print(pos);
			}
			pos.y -= (1 + 0.15f );
		}

		blackSize = blocks [0, 0].collider.bounds.size.x;


		//blocks[0, 0].transform.position = pos;
		//screenSize = Screen.width;
		//print ("screen:"+Camera.main.renderer.bounds.size.x);
		//print (blackSize);
	}
}
