using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	static public Scoreboard	SB;

	private int 			score = 0;
	private int 			highest = 0;

	private string 			HIGHEST = "highestScore";
	private Vector2			labelSize;
	private GUIStyle		labelStyle;


	void OnGUI () {

		labelStyle.alignment = TextAnchor.MiddleRight;
		GUI.Label (new Rect(Screen.width/3 - labelSize.x/2, Screen.height/20, labelSize.x, labelSize.y), ""+score, labelStyle);

		labelStyle.alignment = TextAnchor.MiddleLeft;
		GUI.Label (new Rect(Screen.width/3*2 - labelSize.x/2, Screen.height/20, labelSize.x, labelSize.y), ""+highest, labelStyle);
	}

	void Awake() {
		SB = this;

		if (PlayerPrefs.HasKey (HIGHEST)) {
			highest = PlayerPrefs.GetInt(HIGHEST);
		}
		PlayerPrefs.SetInt (HIGHEST, highest);


		labelSize = new Vector2 (Screen.width/4, Screen.height/15);
		labelStyle = new GUIStyle ();
		labelStyle.normal.background = null;
		labelStyle.fontSize = Screen.height/15;
	}

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void FixedUpdate () {

		if (score > highest) {
			highest = score;
			PlayerPrefs.SetInt (HIGHEST, highest);
		}

	}

	public void ScoreUp(int value) {
		score += value;
	}

	void OnDestroy() {
		//PlayerPrefs.DeleteAll ();
	}
}
