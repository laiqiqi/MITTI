using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour {
	private StatePatternAI AI;

	void Start() {
		AI = this.transform.GetComponentInParent<StatePatternAI>();
	}

	// public void OnCollisionEnter(Collision collision){
	// 	AI.bodyColInfo = collision;

	// 	if(AI.currentState == AI.slamState){
	// 		if(collision.collider.tag == "Player"){
	// 			Debug.Log("Slam Player");
	// 			AI.player.transform.Find("PlayerBody").GetComponent<PlayerStat>().health -= 2f;
	// 			AI.camRig.transform.position = AI.transform.position + AI.transform.forward*2f;
	// 		}
	// 	}
	// }

	// public void OnTriggerEnter(Collider col){
	// 	// AI.bodyColInfo = col;

	// 	if(AI.currentState == AI.slamState){
	// 		if(col.tag == "Player"){
	// 			Debug.Log("Slam Player");
	// 			AI.player.transform.Find("PlayerBody").GetComponent<PlayerStat>().health -= 2f;
	// 			AI.camRig.transform.position = AI.transform.position + AI.transform.forward*2f;
	// 		}
	// 	}
	// }
}
