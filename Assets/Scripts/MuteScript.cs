using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MuteScript : MonoBehaviour {
	public Material[]		mats;			

	//0: sound		1: mute
	private int			mute;

	private Image		image;


	void Awake() {
		image = gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		mute = GameSettings.GS.mute;
		UpdateMaterials();
	}


	public void ToggleMute() {
		mute = GameSettings.GS.mute;
		UpdateMaterials();
	}

	private void UpdateMaterials() {
		image.material = mats[mute];
	}


	void OnEnable() {
		if (mute != GameSettings.GS.mute) {
			mute = GameSettings.GS.mute;
			UpdateMaterials();
		}
	}

}
