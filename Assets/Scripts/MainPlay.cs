using UnityEngine;
using System.Collections;

public class MainPlay : MonoBehaviour {

	public GameObject			blockPrefab;


	//public GameObject
	public GameObject[,]	blocks;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void initGrid() {
		blocks = new GameObject[10, 10];

		for (int i=0; i<10; i++) {
			for(int j=0; j<10; j++) {
				blocks[i,j] = Instantiate();
			}
		}
	}
}
