using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBox : MonoBehaviour {
	private bool CanSlash = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col){
		Debug.Log("Big box = trigger enter");
	}
	void OnTriggerExit(Collider other)
    {
        Debug.Log("Big box = trigger exit");
		CanSlash = true;
	}
	public void SetCanSlash(bool f){
		CanSlash = f;
	}
	public bool GetCanSlash(){
		return CanSlash;
	}
}
