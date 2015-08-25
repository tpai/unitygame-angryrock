using UnityEngine;
using System.Collections;

public class ResultCoin : MonoBehaviour {

	[SerializeField] Animator[] anims;

	public void Show () {
		StartCoroutine ("ShowCoin");
	}

	IEnumerator ShowCoin () {
		int counter = 0;
		foreach (Animator anim in anims) {
			if (counter == PlayerPrefs.GetInt ("DestroyBirds"))break;
			anim.SetBool ("Show", true);
			counter ++;
			yield return new WaitForSeconds (1f);
		}
	}
}
