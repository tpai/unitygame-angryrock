using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.collider.tag == "Obstacle") {
			float hurt = anim.GetFloat ("Hurt");
			float plus = coll.collider.GetComponent<Obstacle> ().hurt;
			anim.SetFloat ("Hurt", hurt + plus);
		}
	}
}
