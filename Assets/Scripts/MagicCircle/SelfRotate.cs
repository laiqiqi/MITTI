using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

	private Color color;
	public bool isOkColor;
	
	// Use this for initialization
	void Awake () {
		color = this.GetComponent<Renderer>().material.GetColor("_TintColor");
		color.a = 0;
		this.GetComponent<Renderer>().material.SetColor("_TintColor", color);
		isOkColor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(StatePatternAI.instance.health <= 0){
			Destroy(this.gameObject);
		}
		
		if(color.a < 1f && !isOkColor){
			this.transform.Rotate(0, 1, 0);
			color.a += Time.deltaTime * 0.5f;
			this.GetComponent<Renderer>().material.SetColor("_TintColor", color);
		}
		else if(color.a >= 0.5f){
			this.transform.Rotate(0, 1, 0);
			isOkColor = true;
		}
		
	}
}
