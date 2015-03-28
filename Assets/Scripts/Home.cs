using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

	public Vector2		btnPos0;
	public Vector2		btnSize;
	//private GUIStyle	btnStyle;


	void OnGUI () {
		if (GUI.Button (new Rect (btnPos0.x, btnPos0.y, btnSize.x, btnSize.y), "Play")) {
			Application.LoadLevel("Play");
		}
	}

	void Awake() {
		btnSize = new Vector2(Screen.width/2, Screen.width/4);
		btnPos0 = new Vector2 (Screen.width/2 - btnSize.x/2, Screen.height/2-btnSize.y);
//		btnStyle = new GUIStyle();
//		btnStyle.normal.background = null;
//		btnStyle.alignment = TextAnchor.MiddleCenter;
//		btnStyle.fontSize = (int)(btnSize.y * 0.8f);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
