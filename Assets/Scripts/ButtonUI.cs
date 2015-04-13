using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonUI : MonoBehaviour {
	public Material[]		mats;	
//	public bool				isGameSetting;
	public string			btnType;

	//0: sound		1: mute
	private int			value;

	private Image		image;



	private void UpdateValue() {
//		print("former:"+value);
		switch(btnType) {
		case "mute":
			value = GameSettings.GS.mute;
			break;

		case "level":
			value = GameSettings.GS.level;
			break;
		}
//		print("alfter:"+value);
	}

	void Awake() {
		image = gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		//value = getValue();
		UpdateValue();
		UpdateMaterials();
	}


	public void ToggleValue() {
		//value = getValue();
		//print("value:"+value);
		UpdateValue();
		UpdateMaterials();
	}

	private void UpdateMaterials() {
		image.material = mats[value];
	}


	void OnEnable() {
		if (GameSettings.GS != null && value != GameSettings.GS.mute) {
			//value = getValue();
			UpdateValue();
			UpdateMaterials();
		}
	}


}
