using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	// Use this for initialization
	private float health;
	public TextMesh text;
	void Start () {
		health = 100f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "AI"){
			health -= 5;
			text.text = health.ToString();
		}
	}
}
