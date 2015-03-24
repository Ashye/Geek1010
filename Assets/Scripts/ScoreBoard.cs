using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreBoard : MonoBehaviour {
	
	public bool					_______________;

	static public int 			score = 0;
	static public int 			highest = 100;
	private Text				tScore;
	private Text				tHighest;

	void Awake() {

		if (PlayerPrefs.HasKey ("score_highest")) {
			highest = PlayerPrefs.GetInt("score_highest");
		}

		Text[] gos = this.GetComponentsInChildren<Text> ();
		if (gos.Length == 2) {
			if (gos[0].name == "Score") {
				tScore = gos[0];
				tHighest = gos[1];
			}else {
				tScore = gos[1];
				tHighest = gos[0];
			}
		}

		PlayerPrefs.SetInt ("score_highest", highest);
	}

	// Use this for initialization
	void Start () {
		if (tHighest != null) {
			tHighest.text = ""+highest;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (score > highest) {
			highest = score;
			PlayerPrefs.SetInt ("score_highest", highest);
			tHighest.text =  "" +highest;
		}

		tScore.text = "" +score;
	}

	void OnDestroy() {
		PlayerPrefs.DeleteAll();
	}
}
