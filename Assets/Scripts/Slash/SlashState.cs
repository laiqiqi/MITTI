using UnityEngine;
using System.Collections;

public class SlashState : AIState {
	private readonly StatePatternAI AI;
	public string name{get;}
	private float startYAngle;
	private int slashCount;
	private Vector3 oldSwordDirection;
	private float timeCount;
	private float angularVelocityAfterParry;

	public SlashState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		name = "SlashState";
	}

	public void StartState(){
		AI.currentState = AI.slashState;
		AI.swordDirection = Mathf.Pow (-1, Random.Range (0, 2)) * AI.swordDirection;
		timeCount = 0f;
		angularVelocityAfterParry = 0f;
		AI.isHit = false;

	}

	public void UpdateState(){
		AI.GetComponent<Rigidbody> ().AddTorque (AI.transform.up * 600);
		timeCount += Time.deltaTime;
//		Debug.Log ("velocity " + AI.GetComponent<Rigidbody> ().angularVelocity.magnitude);
//		Debug.Log ("isHit " + AI.isHit);
//		Debug.Log ("changeState " + (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 2f && AI.isHit == true));

//		if(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > 2f && AI.isHit == true){
//			AI.currentState.EndState ();
//			AI.escapeState.StartState ();
//		}

		if(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > 2f && AI.isHit == true){
			angularVelocityAfterParry = AI.GetComponent<Rigidbody> ().angularVelocity.magnitude;
		}

		Debug.Log ("AfterParry "+(angularVelocityAfterParry > 2f));
		Debug.Log ("isHit "+(AI.isHit));
		Debug.Log ("V "+(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1.5f));
		if(angularVelocityAfterParry > 2f && AI.isHit == true && AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1.5f){
			Debug.Log ("count");
			AI.currentState.EndState ();
			AI.escapeState.StartState ();
		}
	}

	public void EndState(){
		AI.isHit = false;
	}

	public void StateChangeCondition(){
		//		AI.floatingState.StartState ();
		AI.escapeState.StartState ();
	}
}
