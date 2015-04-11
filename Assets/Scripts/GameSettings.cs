using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {
	public static GameSettings			GS;
	public static string				muteSetStr = "muteSetting";


	private int							_mute;


	void Awake() {
		GS = this;
		_mute = PlayerPrefs.GetInt(muteSetStr);
	}


	public void ToggleMute() {
		if (_mute == 0) {
			_mute = 1;
		}else {
			_mute = 0;
		}
		PlayerPrefs.SetInt(muteSetStr, _mute);

	}

	public int mute {
		get {
			return _mute;
		}
	}

}
