using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeHScore : MonoBehaviour {

	private Vector2			lablePos;
	private Vector2			labelSize;
	private int				hScore;
	private int			fontSize;



	void OnGUI() {
		GUI.color = new Color(46f/255f ,1f,248/255f);
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.fontSize = fontSize;
		GUI.Label(new Rect(lablePos.x, lablePos.y, labelSize.x, labelSize.y), hScore.ToString());
	}

	void Awake() {
		if (PlayerPrefs.HasKey(Scoreboard.HIGHEST)) {
			hScore = PlayerPrefs.GetInt(Scoreboard.HIGHEST);
		}else {
			hScore = 0;
			PlayerPrefs.SetInt(Scoreboard.HIGHEST, hScore);
		}


		fontSize = (int)(Screen.width/8);
		labelSize = new Vector2(hScore.ToString().Length * fontSize, fontSize+4);


	}

	// Use this for initialization
	void Start () {

		Vector3 tmp = transform.position;
		tmp.y -=1.5f;

		Vector3 aaa = Camera.main.WorldToScreenPoint(tmp);

		lablePos = new Vector2(Screen.width/2 - labelSize.x/2 - 3, Screen.height -  aaa.y);
		//print(lablePos);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
