using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public delegate void Shooting();
	public event Shooting Hold, Shot, Reset;

	public int numberOfRock = 3;
	public float shootForce = 300f;
	bool shot = false;
	bool hold = false;
	Rigidbody2D rigid;

	void OnEnable () {
		shot = false;
		rigid = GetComponent<Rigidbody2D> ();
		rigid.gravityScale = 0;
		rigid.angularVelocity = 0f;
		rigid.velocity = Vector3.zero;

		transform.position = GameObject.Find ("RockSpawner").transform.position;
		if (Reset != null)
			Reset ();
	}

	Vector2 center;

	void Start () {
		center = transform.position;
	}

	void FixedUpdate () {

		if (shot)
			return;

		if (!hold)
			return;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		// input limit by box
//		transform.position = new Vector3 (
//			Mathf.Clamp(mousePos.x, -8f, -5f),
//			Mathf.Clamp(mousePos.y, -2.8f, 2.8f),
//			0f
//		);
		// input limit by circle
		Vector2 position = new Vector2 (mousePos.x, mousePos.y);
		position = Vector2.ClampMagnitude (position - center, 3f);
		transform.position = center + position;
	}

	void OnMouseDown () {
		if (shot)
			return;

		if (Hold != null)
			Hold ();
		hold = true;
	}

	void OnMouseUp () {
		if (shot)
			return;

		Shoot ();
		hold = false;
	}

	void Shoot () {
		if (Shot != null)
			Shot ();

		shot = true;
		Transform slingshot = GameObject.Find ("Slingshot").transform;
		rigid.AddForce ((slingshot.position - transform.position) * shootForce);
		rigid.gravityScale = 1;
		numberOfRock --;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.collider.tag == "Obstacle") {
			InvokeRepeating ("CheckIfSlowdown", 3f, 1f);
		}
	}

	void CheckIfSlowdown () {
		if (Mathf.Abs(rigid.velocity.x) < 5f) {
			this.enabled = false;
			Invoke ("ResetRock", 1f);
			CancelInvoke ("CheckIfSlowdown");
		}
	}

	void ResetRock () {
		if (numberOfRock == 0) {
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<CircleCollider2D> ().enabled = false;
		}
		else {
			this.enabled = true;
		}
	}
}
