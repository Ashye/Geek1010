using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	static public Scoreboard	SB;

	private int 			score = 0;
	private int 			highest = 0;

	private string 			HIGHEST = "highestScore";
	private Text			tScore;
	private Text			tHighest;

	void Awake() {
		SB = this;

		Text[] texts = (transform.GetChild(0)).GetComponentsInChildren<Text>();
		tScore = texts [0];
		tHighest = texts [1];


		if (PlayerPrefs.HasKey (HIGHEST)) {
			highest = PlayerPrefs.GetInt(HIGHEST);
		}
		PlayerPrefs.SetInt (HIGHEST, highest);
	}

	// Use this for initialization
	void Start () {
		tHighest.text = highest.ToString ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (score > highest) {
			highest = score;
			PlayerPrefs.SetInt (HIGHEST, highest);
			tHighest.text = highest.ToString ();
		}

		tScore.text = score.ToString ();
	}

	public void ScoreUp(int value) {
		score += value;
	}

	void OnDestroy() {
		//PlayerPrefs.DeleteAll ();
	}
}
