using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTarget : MonoBehaviour {

	public TutorialAIStatePattern tutorAI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter (Collider col){
		if(col.tag.Equals("Player")){
			tutorAI.dashCount += 1;
			Destroy(this.gameObject);
		}
	}
}
