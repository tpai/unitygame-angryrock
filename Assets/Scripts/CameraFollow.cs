using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float scrollSpeed = 1f;
	bool shot = false;
	bool hold = false;

	void OnEnable () {
		target.GetComponent<Rock>().Shot += HandleShot;
		target.GetComponent<Rock>().Hold += HandleHold;
	}

	void HandleHold () {
		hold = true;
	}

	void HandleShot () {
		shot = true;
	}

	void Update () {
		if (!hold && Input.GetMouseButton (0)) {
			transform.Translate (
				-1f * new Vector3 (
					Input.GetAxis ("Mouse X") * scrollSpeed,
					0f,
					0f
				)
			);
			transform.position = new Vector3 (
				Mathf.Clamp (
					transform.position.x, 
					0f, 
					float.MaxValue
				), 
				transform.position.y,
				transform.position.z
			);
		}
	}
		
	void FixedUpdate () {
		if (shot) {
			transform.position = Vector3.Lerp (
				transform.position,
				new Vector3 (
					Mathf.Clamp (target.position.x, 0f, float.MaxValue),
					transform.position.y,
					-10f
			),
				Time.deltaTime * 5f
			);
		}
	}
}
