using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArrowCheck : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider col){
		//on check for arrows
		if(col.gameObject.tag == "projectile"){
			Debug.Log(col.gameObject.name + " enters " + gameObject.name);
			col.gameObject.SendMessage( "EntersAIArrowRange", SendMessageOptions.DontRequireReceiver );
		}
		//tell the upper dude that the arrow can deal dmg
	}
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "projectile"){
			Debug.Log(col.gameObject.name + " exits " + gameObject.name);
			col.gameObject.SendMessage( "ExitsAIArrowRange", SendMessageOptions.DontRequireReceiver );
		}
	}
}
