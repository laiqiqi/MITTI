using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateTextHeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void updatePlayerHealth(float currentHealth){
		GetComponent<TextMesh>().text = currentHealth.ToString();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
