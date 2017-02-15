using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

	private Color color;
	
	// Use this for initialization
	void Awake () {
		color = this.GetComponent<Renderer>().material.GetColor("_TintColor");
		color.a = 0;
		this.GetComponent<Renderer>().material.SetColor("_TintColor", color);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0, 1, 0);
		color.a += Time.deltaTime * 0.5f;
		this.GetComponent<Renderer>().material.SetColor("_TintColor", color);
	}
}
