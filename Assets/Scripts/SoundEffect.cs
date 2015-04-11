using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {
	public static SoundEffect		SE;

	public AudioClip 				dropClip;
	public AudioClip				gameoverClip;
	public AudioClip				gobackClip;
	public AudioClip				gridClearClip;
	public AudioClip				shapeShowUpClip;



	void Awake() {
		SE = this;

	}

	public void MakeDropSound() {
		MakeSound(dropClip);
	}

	public void MakeGameoverSound() {
		MakeSound(gameoverClip);
	}

	public void MakeCanntDropSound() {
		MakeSound(gobackClip);
	}

	public void MakeGridClearSound() {
		MakeSound(gridClearClip);
	}

	public void MakeShapeShowUpSound() {
		MakeSound(shapeShowUpClip);
	}

	private void MakeSound(AudioClip clip) {
		if (0 == GameSettings.GS.mute) {
			AudioSource.PlayClipAtPoint(clip, transform.position);
		}
	}
}
