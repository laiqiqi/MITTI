using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwordRadiusCheck : MonoBehaviour {

	// Use this for initialization
	// void OnTriggerEnter(Collider col){
	// 	if(col.gameObject.tag == "playersword"){
	// 		col.gameObject.SendMessage( "SetAbleHitAI", SendMessageOptions.DontRequireReceiver );
	// 	}
	// }
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "playersword"){
			col.gameObject.SendMessage( "SetAbleHitAI", true, SendMessageOptions.DontRequireReceiver );
			Debug.Log(col.gameObject.name + " exits " + gameObject.name);
		}
	}
}
