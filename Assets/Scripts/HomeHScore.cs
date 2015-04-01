using UnityEngine;
using System.Collections;

public class HomeHScore : MonoBehaviour {

	private Vector3			pos;
	private int				hScore;

	void OnGUI () {
		GUI.skin.label.fontSize = Screen.width/8;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		//GUI.Label(new Rect(Screen.width/2- Screen.width/4 - 2, -25, Screen.width/2, 200), hScore.ToString());

		//GUI.Label(new Rect(Screen.width/2- Screen.width/4 - 4, 0 , Screen.width/2, 200), hScore.ToString());

		GUI.Label(new Rect(Screen.width/2- Screen.width/4 - 2, Screen.height - pos.y, Screen.width/2, 200), hScore.ToString());
	}


	void Awake() {
		if (PlayerPrefs.HasKey(Scoreboard.HIGHEST)) {
			hScore = PlayerPrefs.GetInt(Scoreboard.HIGHEST);
		}else {
			hScore = 0;
			PlayerPrefs.SetInt(Scoreboard.HIGHEST, hScore);
		}
	}

	// Use this for initialization
	void Start () {
		Vector3 tmp = new Vector3(-0.4f, 11f, 0);
		print(tmp);
//
//		//Camera.main 调整了高度，所以需要把调整后的高度还原再计算，才会对应到屏幕
//		tmp.y += (-1f);
//		tmp.x += 0.4f;
////
		pos = Camera.main.WorldToScreenPoint(tmp);
		print("screen:"+Screen.height);
		print(pos);

		//pos = new Vector2();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
