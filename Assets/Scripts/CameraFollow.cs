using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	void Start () {
		
	}
	
	void FixedUpdate () {
		transform.position = Vector3.Lerp (
			transform.position,
			new Vector3 (
				Mathf.Clamp(target.position.x, 0f, float.MaxValue),
				transform.position.y,
				-10f
			),
			Time.deltaTime * 10f
		);
	}
}
