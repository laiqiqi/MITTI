using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordFloatingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private float agility;
	private Transform target;
	public List<AIState> choice{ get;set; }

	public SwordFloatingAIState(AITest statePatternAI){
		AI = statePatternAI;
		target = new GameObject ().transform;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.swordFloatingAIState;
		speed = 10;
		agility = 100;
		//		AI.currentState = AI.stopState;
		//		AI.GetComponent<Rigidbody>().isKinematic = true;

		foreach(GameObject sc in AI.swordController){
			sc.transform.GetChild (0).GetComponent<AISword> ().state = 1;
		}

		target.position = new Vector3 (AI.player.transform.position.x + Random.Range (-10f, 10f),
			AI.player.transform.position.y + Random.Range (0f, 2f),
			AI.player.transform.position.z + Random.Range (-10f, 10f));
	}

	public void UpdateState(){
		Debug.Log ("floating");
		foreach(GameObject sc in AI.swordController){
			sc.transform.Rotate (0f, agility * Time.deltaTime, 0f);
//			sc.GetComponent<Rigidbody> ().AddTorque (-AI.transform.up * speed*Time.deltaTime);
		}
		Floating ();
		AI.transform.LookAt (AI.player.transform);
	}

	public void EndState(){
		foreach(GameObject sc in AI.swordController){
			sc.transform.GetChild (0).GetComponent<AISword> ().state = 2;
		}
	}

	public void StateChangeCondition(){
		EndState ();
		AI.swordSlashingAIState.StartState ();
	}

	void Floating(){
		float step = speed * Time.deltaTime;
		AI.transform.position = Vector3.MoveTowards(AI.transform.position, target.position, step);
		if(Vector3.Distance(AI.transform.position, target.position) < 0.1f){
			//It is within ~0.1f range, do stuff
			target.position = new Vector3 (AI.player.transform.position.x + Random.Range (-10f, 10f),
				AI.player.transform.position.y + Random.Range (1f, 2f),
				AI.player.transform.position.z + Random.Range (-10f, 10f));
//			EndState ();
			// AI.slashState.StartState ();

		}
//		Debug.Log (Vector3.Distance(AI.transform.position, AI.player.transform.position));
		if(Vector3.Distance(AI.transform.position, AI.player.transform.position) < 5f){
			StateChangeCondition();
		}
	}
}
