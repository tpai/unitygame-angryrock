﻿using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public delegate void Shooting();
	public event Shooting Shot, test;

	bool hold = false;
	public float shootForce = 300f;

	void Start () {
	
	}
	
	void FixedUpdate () {

		if (!hold)
			return;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (mousePos.x, mousePos.y, 0f);
	}

	void OnMouseDown () {
		hold = true;
	}

	void OnMouseUp () {
		hold = false;
		Shoot ();
	}

	void Shoot () {
		if (Shot != null)
			Shot ();
		Transform slingshot = GameObject.Find ("Slingshot").transform;
		GetComponent<Rigidbody2D> ().AddForce ((slingshot.position - transform.position) * shootForce);
		GetComponent<Rigidbody2D> ().gravityScale = 1;
	}
}
