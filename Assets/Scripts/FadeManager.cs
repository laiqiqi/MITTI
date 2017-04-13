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
		//useful for cube with material/specular
//		float lerp = Mathf.PingPong (Time.time, duration) / duration;
//		alpha = Mathf.Lerp(0.0f, 1.0f, lerp);
//		this.GetComponent<MeshRenderer> ().material.color = new Color (this.GetComponent<MeshRenderer> ().material.color.r
//			, this.GetComponent<MeshRenderer> ().material.color.g, this.GetComponent<MeshRenderer> ().material.color.b, alpha);
		
//		Color red = this.GetComponent<MeshRenderer> ().material.color;
//		red = new Color (red.g, red.r, red.b, alpha);
//		this.GetComponent<MeshRenderer> ().material.color = new Color (this.GetComponent<MeshRenderer> ().material.color.r
//			, this.GetComponent<MeshRenderer> ().material.color.g, this.GetComponent<MeshRenderer> ().material.color.b, alpha);
//		Debug.Log (this.GetComponent<MeshRenderer> ().material.color);
//		this.GetComponent<MeshRenderer> ().material.color.a = alpha;
//				renderer.material.color.a = alpha;

		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		alpha = Mathf.Lerp(0.0f, 1.0f, lerp);
		foreach(Material m in this.GetComponent<MeshRenderer> ().materials){
			m.color = new Color (m.color.r, m.color.g, m.color.b, alpha);
//			m.SetAlpha(m.color.a - .1F);
		}
	}
}

public static class ExtensionMethods {


	public static void SetAlpha (this Material material, float value) {
		Color color = material.color;
		color.a = value;
		material.color = color;
	}


}