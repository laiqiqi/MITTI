using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

	private float duration;
	private float alpha;
	void Start(){
		duration = 1.0f;
		alpha = 0.0f;
	}

	void Update(){
		lerpAlpha();
	}

	void lerpAlpha () {

		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		alpha = Mathf.Lerp(0.0f, 1.0f, lerp);
//		Color red = this.GetComponent<MeshRenderer> ().material.color;
//		red = new Color (red.g, red.r, red.b, alpha);
		this.GetComponent<MeshRenderer> ().material.color = new Color (this.GetComponent<MeshRenderer> ().material.color.r
			, this.GetComponent<MeshRenderer> ().material.color.g, this.GetComponent<MeshRenderer> ().material.color.b, alpha);
//		Debug.Log (this.GetComponent<MeshRenderer> ().material.color);
//		this.GetComponent<MeshRenderer> ().material.color.a = alpha;
//				renderer.material.color.a = alpha;
	}
}