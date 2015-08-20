using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public delegate void Shooting();
	public event Shooting Hold, Shot, Reset;

	public float shootForce = 300f;
	bool hold = false;
	Rigidbody2D rigid;

	void OnEnable () {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.gravityScale = 0;
		rigid.angularVelocity = 0f;
		rigid.velocity = Vector3.zero;

		transform.position = GameObject.Find ("RockSpawner").transform.position;
	}

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
		rigid.AddForce ((slingshot.position - transform.position) * shootForce);
		rigid.gravityScale = 1;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.collider.tag == "Obstacle") {
			InvokeRepeating ("CheckIfSlowdown", 2f, 1f);
		}
	}

	void CheckIfSlowdown () {
		if (Mathf.Abs(rigid.velocity.x) < 5f) {
			this.enabled = false;
			Invoke ("ResetRock", .5f);
			CancelInvoke ("CheckIfSlowdown");
		}
	}

	void ResetRock () {
		if (Reset != null)
			Reset ();
		this.enabled = true;
	}
}
