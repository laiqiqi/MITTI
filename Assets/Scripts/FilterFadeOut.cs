using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterFadeOut : MonoBehaviour {
	private Color color;

	// Use this for initialization
	void Start () {
		color = this.GetComponent<Renderer>().material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		FilterDisappear();
	}

	void FilterDisappear() {
		if(this.GetComponent<Renderer>().material.GetColor("_Color").a > 0f){
			color.a -= 0.01f;
			this.GetComponent<Renderer>().material.SetColor("_Color", color);
		}
		// else{
		// 	Destroy(this.gameObject);
		// }
	}
}
