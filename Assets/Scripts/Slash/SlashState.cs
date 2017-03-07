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
	private bool isStart;
	private int countState;
	private float oldVelocity;

	public SlashState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		name = "SlashState";
	}

	public void StartState(){
		AI.AttachSword();
		AI.currentState = AI.slashState;
		AI.transform.GetChild(1).GetComponent<Rigidbody> ().isKinematic = false;
//		AI.transform.LookAt (AI.player.transform);
//		AI.transform.Rotate (0f, 0f, Random.Range(0f, 360f));
		// AI.transform.GetChild(1).GetComponent<Rigidbody> ().isKinematic = false;
		isStart = false;
		AI.isHit = false;
		timeCount = 0;
		countState = 0;
		oldVelocity = 0;

		AI.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;

//		AI.swordDirection = Mathf.Pow (-1, Random.Range (0, 2)) * AI.swordDirection;
//		timeCount = 0f;
//		angularVelocityAfterParry = 0f;
//		AI.isHit = false;
//		AI.GetComponent<Rigidbody> ().isKinematic = false;

	}

	public void UpdateState(){
//		Debug.Log ("before : "+AI.transform.rotation);
		AI.GetComponent<Rigidbody> ().AddTorque (-AI.transform.up * 200);
//		Debug.Log ("after : "+AI.transform.rotation);
//		AI.GetComponent<Rigidbody> ().AddTorque (new Vector3(0f,1f,0f).normalized * 600f);
		timeCount += Time.deltaTime;
//		Debug.Log ("velocity " + AI.GetComponent<Rigidbody> ().angularVelocity.magnitude);
//		Debug.Log ("isHit " + AI.isHit);
//		Debug.Log ("changeState " + (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 2f && AI.isHit == true));

//		if(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > 2f && AI.isHit == true){
//			AI.currentState.EndState ();
//			AI.escapeState.StartState ();
//		}

//		if(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > 2f && AI.isHit == true){
//			angularVelocityAfterParry = AI.GetComponent<Rigidbody> ().angularVelocity.magnitude;
//		}

//		if(AI.isParry){
//			AI.GetComponent<Rigidbody> ().AddTorque (-AI.transform.up * 5000);
//			AI.isParry = false;
//		}


		if (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > 1f) {
			isStart = true;
		}
		if (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1f && AI.isHit) {
			isStart = false;
		}

//		if (oldVelocity.Count == 0) {
//			oldVelocity.Add (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude);
//		} else if (oldVelocity.IndexOf (oldVelocity.Count - 1) > AI.GetComponent<Rigidbody> ().angularVelocity.magnitude) {
//			float temp = oldVelocity.IndexOf (oldVelocity.Count - 1);
//			oldVelocity.Clear ();
//			oldVelocity.Add (temp);
//			oldVelocity.Add (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude);
//		} else if (oldVelocity.IndexOf (oldVelocity.Count - 1) < AI.GetComponent<Rigidbody> ().angularVelocity.magnitude
//		         && oldVelocity.Count >= 2) {
//			if (oldVelocity.IndexOf (oldVelocity.Count - 2) > oldVelocity.IndexOf (oldVelocity.Count - 1)) {
//				countState = 2;
//			}
//		}else {
//			oldVelocity.Clear ();
//		}


		if (oldVelocity > AI.GetComponent<Rigidbody> ().angularVelocity.magnitude) {
			countState = 1;
		} else if (AI.GetComponent<Rigidbody> ().angularVelocity.magnitude > oldVelocity && countState == 1) {
			countState = 2;
		} else {
			countState = 0;
		}
		oldVelocity = AI.GetComponent<Rigidbody> ().angularVelocity.magnitude;


		if(countState == 2 && isStart && !AI.isHit){
			AI.currentState.EndState ();
			AI.escapeState.StartState();
		}







//		Debug.Log ("AfterParry "+(angularVelocityAfterParry > 2f));
//		Debug.Log ("isHit "+(AI.isHit));
//		Debug.Log ("V "+(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1.5f));
		// if( isStart && !AI.isHit && AI.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1f){
		// 	Debug.Log ("SlashStateChange");
		// 	AI.currentState.EndState ();
		// 	AI.escapeState.StartState ();
		// }
		// if(timeCount >= 3f && !AI.isHit && isStart){
		// 	AI.currentState.EndState ();
		// 	AI.escapeState.StartState();
		// }
//		Debug.Log(AI.transform.GetChild(1).GetComponent<Rigidbody> ().velocity.magnitude);
//		Debug.Log(AI.GetComponent<Rigidbody> ().angularVelocity);
//		Debug.Log ("isStart   : " +isStart);
//		Debug.Log ("!isHit   : " +!AI.isHit);
//		if (isStart && !AI.isHit && AI.transform.GetChild (1).GetComponent<Rigidbody> ().velocity.magnitude < 1.5f) {
//		Debug.Log(AI.transform.GetComponent<Rigidbody> ().GetPointVelocity(AI.transform.GetChild (1).GetChild(2).transform.position).magnitude);
//		Debug.Log(AI.transform.InverseTransformDirection(AI.GetComponent<Rigidbody> ().angularVelocity).magnitude);
//		if (isStart && !AI.isHit && Mathf.Abs(AI.GetComponent<Rigidbody> ().angularVelocity.magnitude) < 1f) {
		if(countState == 2 && isStart && !AI.isHit){
			AI.currentState.EndState ();
			AI.escapeState.StartState();
		}


//		 if(timeCount >= 6f ){
//			 AI.currentState.EndState ();
//			 AI.escapeState.StartState();
//		 }
	}

	public void EndState(){
		AI.isHit = false;
		timeCount = 0;
		AI.transform.GetChild(1).GetComponent<Rigidbody> ().isKinematic = true;
		AI.DetachSword();
//		AI.GetComponent<Rigidbody> ().isKinematic = false;
	}

	public void StateChangeCondition(){
		//		AI.floatingState.StartState ();
		AI.escapeState.StartState ();
	}
}
