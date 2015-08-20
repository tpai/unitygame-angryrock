using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public delegate void Shooting();
	public event Shooting Hold, Shot;

	bool hold = false;
	public float shootForce = 300f;

	void FixedUpdate () {

		if (!hold)
			return;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (
			Mathf.Clamp(mousePos.x, -8f, -5f),
			Mathf.Clamp(mousePos.y, -2.8f, 2.8f),
			0f
		);
	}

	void OnMouseDown () {
		if (Hold != null)
			Hold ();
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
