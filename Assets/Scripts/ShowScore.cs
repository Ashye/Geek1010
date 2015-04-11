using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScore : MonoBehaviour {

	public bool				isGameOver;

	private int				score;
	private Text			tScore;



	void Start() {

		if (isGameOver) {
			score = Scoreboard.SB.currScore;
		}else {
			if (PlayerPrefs.HasKey(Scoreboard.HIGHEST)) {
				score = PlayerPrefs.GetInt(Scoreboard.HIGHEST);
			}else {
				score = 0;
				PlayerPrefs.SetInt(Scoreboard.HIGHEST, score);
			}
		}
		tScore = gameObject.GetComponent<Text>();
		tScore.text = score.ToString();
	}
	
}
