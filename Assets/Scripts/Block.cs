using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public GameObject		blockPrefab;
	public bool 			____________________;

	private bool			_empty = true;
	private int[]			_rowAndCol;
	private Color			_oldColor;

	//show animate
	private Vector3			targetPos;
	private float			moveSpeed = 70f;
	private bool			isAnimate = false;

	//clear Row and Col
	private GameObject		tmpblk = null;
	private float			clearSpeed = 5f;



	// Use this for initialization
	void Start () {

		targetPos = transform.position;
		isAnimate = true;
		transform.position = Vector3.one * 10f;

		_oldColor = renderer.material.color;
		_rowAndCol = getRowACol ();
	}
	
	void FixedUpdate () {
		if (isAnimate) {
			//print("animation");
			if (transform.position != targetPos) {
				transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
			}else {
				isAnimate = false;
			}
		}

		if (tmpblk != null) {
			Vector3 local = tmpblk.transform.localScale;
			if (local.x >0) {
				tmpblk.transform.localScale = local - Vector3.one * clearSpeed * Time.deltaTime;
			}else {
//				Destroy(tmpblk);
//				tmpblk = null;
				tmpblk.SetActive(false);

			}
		}
	}

	private int[] getRowACol() {
		string[] tmp = gameObject.name.Split ('-');
		if (tmp.Length == 3) {
			int [] RA = new int[2];
			RA [0] = int.Parse (tmp [1]);
			RA [1] = int.Parse (tmp [2]);
			return RA;
		}

		return null;

	}

	public bool empty {
		get {
			return _empty;
		}
		set {

			//void the cross point deal with twice problem
			if (value && _empty) {
				return;
			}
			_empty = value;
			//restore color when block is empty
			if (_empty) {
				ClearRowACol();
				//renderer.material.color = _oldColor;
			}
		}
	}

	private void ClearRowACol() {
		Vector3 pos = transform.position;
		pos.z -= 1;

		/* cache tmpblk improve performance */
		if (tmpblk == null) {
//			print("instantiate tmpblk..."+getRowACol()[0]+"-"+getRowACol()[1]);
			tmpblk = Instantiate(blockPrefab, pos, transform.rotation) as GameObject;
			tmpblk.transform.SetParent(transform);
		}else {
			tmpblk.SetActive(true);
			tmpblk.transform.localScale = transform.localScale;
		}
		tmpblk.renderer.material.color = renderer.material.color;

		renderer.material.color = _oldColor;

	}


	public int[] rowACol {
		get {
			return _rowAndCol;
		}
	}
}
