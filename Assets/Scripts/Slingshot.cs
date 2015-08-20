using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	[SerializeField] LineRenderer band1, band2;
	Vector3 bandPoint1, bandPoint2;
	float radius;
	bool shoot = false;
	public Transform target;

	void OnEnable () {
		GameObject.Find ("Rock").GetComponent<Rock>().Shot += HandleShot;
		GameObject.Find ("Rock").GetComponent<Rock>().Reset += HandleReset;
	}

	void HandleReset () {
		shoot = false;
		band1.enabled = true;
		band2.enabled = true;
	}

	void HandleShot () {
		shoot = true;
		band1.SetPosition (1, band1.transform.position);
		band2.SetPosition (1, band2.transform.position);
		band1.enabled = false;
		band2.enabled = false;
	}

	void Start () {
		band1.SetPosition (0, new Vector3 (
			band1.transform.position.x,
			band1.transform.position.y,
			-2f
		));

		band2.SetPosition (0, new Vector3 (
			band2.transform.position.x,
			band2.transform.position.y,
			-1f
		));

		radius = target.GetComponent<CircleCollider2D> ().radius;
	}
	
	void FixedUpdate () {

		if (shoot)
			return;

		bandPoint1 = Vector3.MoveTowards (target.position, transform.position, -radius);

		band1.SetPosition (1, new Vector3 (
			bandPoint1.x,
			bandPoint1.y,
			-2f
		));

		bandPoint2 = Vector3.MoveTowards (target.position, transform.position, radius);

		band2.SetPosition (1, new Vector3 (
			bandPoint2.x,
			bandPoint2.y,
			-1f
		));
	}
}
