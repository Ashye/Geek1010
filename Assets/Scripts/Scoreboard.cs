using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	static public Scoreboard	SB;

	private int 			score = 0;
	private int 			highest = 0;
	private int				upScore = 0;

	private Text			tScore;
	private Text			thScore;

	static public string 	HIGHEST = "highestScore";



	void Awake() {
		SB = this;

		if (PlayerPrefs.HasKey (HIGHEST)) {
			highest = PlayerPrefs.GetInt(HIGHEST);
		}
		PlayerPrefs.SetInt (HIGHEST, highest);

		tScore = transform.GetChild(1).GetComponent<Text>();
		thScore = transform.GetChild(0).GetComponent<Text>();

		thScore.text = highest.ToString();
		tScore.text = score.ToString();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (upScore > 0) {
			score ++;
			upScore--;
			tScore.text = score.ToString();
		}

	}

	public void ScoreUp(int value) {
		upScore += value;
		if (score + upScore > highest) {
			highest = score + upScore;
			thScore.text = highest.ToString();
			PlayerPrefs.SetInt (HIGHEST, highest);
		}
	}

	public int currScore {
		get {
			return score;
		}
	}

	void OnDestroy() {
		//PlayerPrefs.DeleteAll ();
	}
}
