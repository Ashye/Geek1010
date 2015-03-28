using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	private Vector2			posBtnFirst;
	private float			spaceBtwBtn;
	private Vector2			sizeBtn;


	void Awake() {
		sizeBtn = new Vector2(Screen.width/2, Screen.width/4);
		spaceBtwBtn = sizeBtn.y/4*5;

		posBtnFirst = new Vector3(Screen.width/2 - sizeBtn.x/2, Screen.height/2-sizeBtn.y);

	}

	void OnGUI() {
		if (GUI.Button(new Rect(posBtnFirst.x, posBtnFirst.y, sizeBtn.x, sizeBtn.y), "Home")) {
			print("Home page is unfinished......");
		}

		if (GUI.Button(new Rect(posBtnFirst.x, posBtnFirst.y+spaceBtwBtn, sizeBtn.x, sizeBtn.y), "Play")) {
			Application.LoadLevel("Play");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
