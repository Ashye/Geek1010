using UnityEngine;
using System.Collections;

//enum GameSettingKey {
//	MUTE = "muteSetting",
//	LEVEL = "difficultySetting"
//}

public class GameSettings : MonoBehaviour {
	public static GameSettings			GS;


	public static string				muteSetStr = "muteSetting";
	public static string				difficultyStr = "difficultySetting";

	//0: sounded		1: muted
	private int							_mute;
	//0: easy			1: hard
	private int 						_difficulty;


	void Awake() {
	//	if (GS == null) {
			GS = this;
			_mute = PlayerPrefs.GetInt(muteSetStr);
			_difficulty = PlayerPrefs.GetInt(difficultyStr);
	//	}
	}


	public void ToggleMute() {
		if (_mute == 0) {
			_mute = 1;
		}else {
			_mute = 0;
		}
		PlayerPrefs.SetInt(muteSetStr, _mute);

	}

	public void ToggleLevel() {
		if (_difficulty == 0) {
			_difficulty = 1;
		}else {
			_difficulty = 0;
		}
		PlayerPrefs.SetInt(difficultyStr, _difficulty);
	}

	public int mute {
		get {
			return _mute;
		}
	}

	public int level {
		get {
			return _difficulty;
		}
	}
}
