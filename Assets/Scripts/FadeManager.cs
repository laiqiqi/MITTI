using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

	private float duration;
	private float alpha;
	public bool isShow;
	void Start(){
		duration = 1.0f;
		alpha = 1f;
	}

	void Update(){
//		lerpAlpha();
		Debug.Log(isShow);
	}

	public void Fade (float alpha) {
		foreach(Material m in this.GetComponent<MeshRenderer> ().materials){
			m.SetAlpha(m.color.a + alpha);
		}
//		alpha = this.GetComponent<MeshRenderer> ().material.color.a;
		if (this.GetComponent<MeshRenderer> ().material.color.a == 0) {
			isShow = false;
		} else if (this.GetComponent<MeshRenderer> ().material.color.a == 1){
			isShow = true;
		}
			
	}

}

public static class ExtensionMethods {


	public static void SetAlpha (this Material material, float value) {
		Color color = material.color;
		if (value < 0) {
			value = 0;
		} else if(value > 1){
			value = 1;
		}
		color.a = value;
		material.color = color;
//		Debug.Log (material.color.a);
	}


}