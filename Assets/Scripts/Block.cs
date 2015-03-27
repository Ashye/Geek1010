using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	private bool			_empty = true;
	private int[]			_rowAndCol;
	private Color			_oldColor;

	// Use this for initialization
	void Start () {
		_oldColor = renderer.material.color;
		_rowAndCol = getRowACol ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		
	}

	private int[] getRowACol() {
		//string name = gameObject.name;
		string[] tmp = gameObject.name.Split ('-');
		//print (tmp[1]+"-"+tmp[2]);
		int [] RA = new int[2];
		RA [0] = int.Parse (tmp [1]);
		RA [1] = int.Parse (tmp [2]);
		return RA;

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
