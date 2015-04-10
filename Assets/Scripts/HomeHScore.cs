using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeHScore : MonoBehaviour {

	private int				hScore;
	private Text			thScore;



	void Awake() {
		if (PlayerPrefs.HasKey(Scoreboard.HIGHEST)) {
			hScore = PlayerPrefs.GetInt(Scoreboard.HIGHEST);
		}else {
			hScore = 0;
			PlayerPrefs.SetInt(Scoreboard.HIGHEST, hScore);
		}

		thScore = gameObject.GetComponent<Text>();
		thScore.text = hScore.ToString();

	}
	
}
