using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	static public Scoreboard	SB;

	private int 			score = 0;
	private int 			highest = 0;
	private int				upScore = 0;

	static public string 	HIGHEST = "highestScore";
	private Vector2			labelSize;
	private Vector2			posScore;
	private Vector2			posHighest;
//	private GUIStyle		labelStyle;


	void OnGUI () {
		GUI.color = new Color(46, 255, 248);
		GUI.skin.label.fontSize = Screen.width/8;


		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(posScore.x, posScore.y, labelSize.x, labelSize.y), score.ToString());

		GUI.skin.label.fontSize = Screen.width /15;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label (new Rect(posHighest.x, posHighest.y, labelSize.x, labelSize.y), highest.ToString());
	}

	void Awake() {
		SB = this;

		if (PlayerPrefs.HasKey (HIGHEST)) {
			highest = PlayerPrefs.GetInt(HIGHEST);
		}
		PlayerPrefs.SetInt (HIGHEST, highest);


		labelSize = new Vector2 (Screen.width/2, Screen.height/12);

		posScore = new Vector2(Screen.width/2 - labelSize.x/2 , Screen.height / 10);
		posHighest = new Vector2(Screen.width/15, 10);


//		labelStyle = new GUIStyle ();
//		labelStyle.normal.background = null;
//		labelStyle.fontSize = Screen.height/15;

	}

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void FixedUpdate () {

		if (upScore > 0) {
			score ++;
			upScore--;
		}

	}

	public void ScoreUp(int value) {
		upScore += value;
		if (score + upScore > highest) {
			highest = score + upScore;
			PlayerPrefs.SetInt (HIGHEST, highest);
		}
	}

	void OnDestroy() {
		//PlayerPrefs.DeleteAll ();
	}
}
