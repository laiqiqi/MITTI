using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		Debug.Log ("Handle hit");
		this.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
	}
}
