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
	



	// Use this for initialization
	void Start () {

		targetPos = transform.position;
		isAnimate = true;
		transform.position = Vector3.one * 10f;

		_oldColor = renderer.material.color;
		_rowAndCol = getRowACol ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (isAnimate) {
			if (transform.position != targetPos) {
				transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
			}else {
				isAnimate = false;
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
			_empty = value;
			//restore color when block is empty
			if (_empty) {
				renderer.material.color = _oldColor;
			}
		}
	}
	

	public int[] rowACol {
		get {
			return _rowAndCol;
		}
	}
}
