using UnityEngine;
using System.Collections;

public class SwordSlashingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private Vector3[] randomVector;
	private int subState;
	private float timecount;
	private GameObject[] swords;
	private Transform target;
	private bool[] isStart;
	private int[] countState;
	private float[] oldVelocity;
	private Quaternion rotationTemp;
	private int dir;

	public SwordSlashingAIState(AITest statePatternAI){
		AI = statePatternAI;
		target = new GameObject ().transform;
	}

	public void StartState(){
		AI.currentState = AI.swordSlashingAIState;
		speed = 200;
		timecount = 0;
		dir = 1;
		countState = new int[AI.swordController.Length];
		oldVelocity = new float[AI.swordController.Length];
		swords = new GameObject[AI.swordController.Length];
		randomVector = new Vector3[AI.swordController.Length];
		isStart = new bool[AI.swordController.Length];
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			randomVector[i] = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
			Transform sword = sc.transform.GetChild (0);
			swords [i] = sword.gameObject;
			oldVelocity [i] = 0;
			countState [i] = 0;
			isStart [i] = false;
			i++;
		}

	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			int state = sc.GetComponent<AISwordController>().state;
			if (state == 0) {
				Debug.Log ("state     0");
				//Rotate sword
				Vector3 heading = swords [i].transform.position - AI.transform.position;
				float angel = Vector3.Angle (heading, -AI.transform.forward);

				if (Mathf.Abs (angel) < 10) {
					sc.GetComponent<AISwordController> ().state = 1;
				}

				sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);

			} else if (state == 1) {
				Debug.Log ("state     1");
				//Prepare
				SubState1(sc, i);
			} else if (state == 2) {
				Debug.Log ("state     2");
				//Connect with joint
				SubState2(sc, i);

			} else if (state == 3) {
				Debug.Log ("state     3");
				//Slash
//				isStart[i] = false;
//				if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > 5f) {
//					AI.transform.position = Vector3.MoveTowards (AI.transform.position, AI.player.transform.position, 10 * Time.deltaTime);
//				}

//				timecount += Time.deltaTime;
//				if (timecount > 10f) {
//					timecount = 10f;
//				}
//
//				if (swords [i].GetComponent<SwordFloatingSword> ().isHit) {
//					timecount /= 2;
//				}
//
//				int direction = Mathf.RoundToInt (randomVector [i].x / Mathf.Abs (randomVector [i].x));
//				sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * timecount * 100 * direction);
				Substate3 (sc, i);
//				ConditionOfSubstate4(sc, i);

			} else if (state == 4) {
				Debug.Log ("state     4");
//				swords [i].transform.parent = sc.transform;
//				swords [i].GetComponent<Rigidbody> ().isKinematic = true;
//				GameObject.Destroy (sc.GetComponent<FixedJoint>());
////				sc.GetComponent<AISwordController> ().state = 1;
//				SubState1Copy (sc, i);
//				Debug.Log (randomVector[i]);
//				Debug.Log (swords[i].transform.localPosition);

				Substate4(sc, i);
			} else if (state == 5) {
				Debug.Log ("state     5");
				Substate5 (sc, i);
			}
			// if (sc.GetComponent<AISwordController> ().state == -1) {
			// 	Debug.Log("Breakkkkkkkkkk");
			// 	swords [i].GetComponent<Rigidbody> ().useGravity = true;
			// }
			// Debug.Log (randomVector[i]);
//			Debug.Log (AI.transform.forwards);
//			Debug.DrawLine (swords[i].transform.position, Vector3.Cross(AI.transform.forward, swords[i].transform.forward),Color.red);
			Debug.DrawLine (swords[i].transform.position, randomVector[i],Color.red);
			i++;
		}
	}

	public void EndState(){
		AI.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}

	public void SubState1(GameObject sc, int i){
		Vector3 relativePos = sc.transform.position - swords [i].transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		swords [i].transform.rotation = Quaternion.RotateTowards (swords [i].transform.rotation, rotation, speed * Time.deltaTime);

		//				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
		float distance = 3f;
		swords [i].transform.localPosition = Vector3.MoveTowards (swords [i].transform.localPosition, randomVector [i].normalized * distance, speed / 50f * Time.deltaTime);
		if (Vector3.Distance (swords [i].transform.localPosition, randomVector [i].normalized * distance) < 1f
			&& Quaternion.Angle (swords [i].transform.rotation, rotation) < 1f) {
			sc.GetComponent<AISwordController> ().state = 2;
			Debug.Log ("sword localposition  "+swords [i].transform.localPosition +"Distance  "+Vector3.Distance (swords [i].transform.localPosition, randomVector [i].normalized * distance));
//			Debug.Log ("sword position  "+swords [i].transform.position);
			Debug.Log ("randomVector         "+randomVector [i].normalized * distance);

		}
	}

	public void SubState1Copy(GameObject sc, int i){
		Vector3 relativePos = sc.transform.position - swords [i].transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		rotation = Quaternion.Euler (rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
		swords [i].transform.rotation = Quaternion.RotateTowards (swords [i].transform.rotation, rotation, speed * Time.deltaTime);

		//				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
		float distance = 0.1f;
		swords [i].transform.position = Vector3.MoveTowards (swords [i].transform.position, randomVector [i] , speed / 50f * Time.deltaTime);
		if (Vector3.Distance (swords [i].transform.position, randomVector [i]) < 1f
			&& Quaternion.Angle (swords [i].transform.rotation, rotation) < 1f) {
			sc.GetComponent<AISwordController> ().state = 2;
			Debug.Log ("sword localposition  "+swords [i].transform.position +"Distance  "+Vector3.Distance (swords [i].transform.position, randomVector [i]));
			//			Debug.Log ("sword position  "+swords [i].transform.position);
			Debug.Log ("randomVector         "+randomVector[i]);

		}
	}

	public void SubState2(GameObject sc, int i){
		if (sc.transform.childCount != 0) {
			swords [i].transform.parent = null;
			swords [i].GetComponent<SwordFloatingSword> ().state = 2;
			sc.transform.LookAt (swords [i].transform);
			sc.GetComponent<AISwordController> ().state = 3;
			sc.AddComponent<FixedJoint> ();
			sc.GetComponent<FixedJoint> ().connectedBody = swords [i].GetComponent<Rigidbody> ();
								// sc.GetComponent<FixedJoint> ().breakForce = 500;
								// sc.GetComponent<FixedJoint> ().breakTorque = 500;
								// sword.GetComponent<Rigidbody> ().useGravity = true;
			swords [i].GetComponent<Rigidbody> ().isKinematic = false;
			//					Physics.IgnoreLayerCollision (8,8);
			for(int j=0; j < swords.Length; j++){
				if (j != i) {
					Physics.IgnoreCollision (swords[i].GetComponent<Collider>(), swords[j].GetComponent<Collider>());
					//							Physics.IgnoreCollision (swords[i].GetComponent<Collider>(), swords[j].GetComponent<Collider>());
				}
			}
			//					swords [i].transform.parent = sc.transform;
		}
	}

	public void Substate3(GameObject sc, int i){
		timecount += Time.deltaTime;
		if (timecount > 10f) {
			timecount = 10f;
		}

		if (swords [i].GetComponent<SwordFloatingSword> ().isHit) {
			timecount /= 2;
		}
		int direction = -Mathf.RoundToInt ((randomVector [i].x - AI.transform.position.x) / Mathf.Abs (randomVector [i].x - AI.transform.position.x));
//		int direction = -Mathf.RoundToInt ((randomVector [i].x) / Mathf.Abs (randomVector [i].x));
//		int direction = 1;
//		if (randomVector [i].normalized.x <= 0) {
//			direction = -1;
//		} else {
//			direction = 1;
//		}
		sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * timecount * 100 * dir);

//		if (i == 1) {
//			Debug.Log ("angular speed" + sc.GetComponent<Rigidbody> ().angularVelocity.magnitude);
//		}
		if (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 1.5f) {
			isStart[i] = true;
		}
		if (swords[i].GetComponent<SwordFloatingSword>().isHit) {
			isStart[i] = false;
		}

		if (oldVelocity[i] - sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 0.01f) {
			countState[i] = 1;
		} else if (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude - oldVelocity[i] > 0.01f && countState[i] == 1) {
			countState[i] = 2;
		} else {
			countState[i] = 0;
		}
		oldVelocity[i] = sc.GetComponent<Rigidbody> ().angularVelocity.magnitude;

//		Debug.Log ("oldVelocity     "+oldVelocity[i]);
		if(countState[i] == 2 && isStart[i] && !swords[i].GetComponent<SwordFloatingSword>().isHit){
//			if (i == 1) {
//				Debug.LogError ("Slash again!");
//			}
			isStart [i] = false;
			dir = (int)Mathf.Pow (-1, Random.Range (1, 3));
			Vector3 AIRandomForward = -AI.transform.forward + AI.transform.right * Random.Range (1f, 5f) * -dir + AI.transform.up * Random.Range (0f, 5f);
//			Vector3 AIRandomForward = -AI.transform.forward + AI.transform.right * Random.Range (1f, 5f) * Mathf.Pow (-1, Random.Range (1, 3)) + AI.transform.up * Random.Range (0f, 5f);
//			Vector3 AIRandomForward = new Vector3 (AI.transform.forward.x*Random.Range(1f, 5f)*Mathf.Pow(-1, Random.Range(1, 3)), AI.transform.forward.y*Random.Range(1f, 5f), AI.transform.forward.z);
//			Vector3 AIRandomForward = new Vector3 (AI.transform.forward.x + Random.Range(-5f,5f),AI.transform.forward.y + Random.Range(0f,5f), AI.transform.forward.z- 2f);
			randomVector[i] = AI.transform.position + AIRandomForward;
//			randomVector[i] = new Vector3 (sc.transform.position.x + Random.Range(-5f,5f),sc.transform.position.y + Random.Range(0f,5f), sc.transform.position.z- 5f);

			//
//			randomVector[i] = new Vector3 (AI.transform.position.x + Random.Range(-5f,5f),AI.transform.position.y + Random.Range(0f,5f), AI.transform.position.z- 5f);
////			randomVector[i] = -AI.transform.forward + new Vector3 (Random.Range(-5f,5f),Random.Range(0f,5f), 0);
//			Vector3 targetLook = Vector3.Cross(AI.player.transform.forward, swords[i].transform.forward) - swords[i].transform.position;
//			Quaternion rotation = Quaternion.LookRotation (targetLook);
//			rotation *= Quaternion.FromToRotation(Vector3.forward, Vector3.right);
//			rotationTemp = rotation;
			sc.GetComponent<AISwordController> ().state = 4;

//			Debug.Log (randomVector[i]);
		}

		if (swords[i].GetComponent<SwordFloatingSword>().isHitOther) {
//			Vector3 targetLook = Vector3.Cross(AI.player.transform.forward, swords[i].transform.forward) - swords[i].transform.position;
//			Quaternion rotation = Quaternion.LookRotation (targetLook);
////			Quaternion rotation = Quaternion.LookRotation (Vector3.Cross(AI.player.transform.forward, swords[i].transform.forward));
//			rotation *= Quaternion.FromToRotation(Vector3.forward, Vector3.right);
//			rotationTemp = rotation;
			sc.GetComponent<AISwordController> ().state = 4;
		}
	}

	public void ConditionOfSubstate4(GameObject sc, int i){
		Debug.Log ("angular speed" + sc.GetComponent<Rigidbody> ().angularVelocity.magnitude);
		Debug.Log ("Start        " + isStart[i]);
		if (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 1.5f) {
			isStart[i] = true;
		}
		if (swords[i].GetComponent<SwordFloatingSword>().isHit) {
			isStart[i] = false;
		}
		if(sc.GetComponent<Rigidbody> ().angularVelocity.magnitude < 1f && isStart[i] == true){
			isStart [i] = false;
			sc.GetComponent<AISwordController> ().state = 4;
		}

//		if (swords[i].GetComponent<SwordFloatingSword>().isHitOther) {
//			sc.GetComponent<AISwordController> ().state = 4;
//		}
		
	}

	public void Substate4(GameObject sc, int i){
		Vector3 relativePos = randomVector[i] - sc.transform.position;
//		relativePos.z = 0;
//		sc.GetComponent<Rigidbody> ().isKinematic = true;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		sc.transform.rotation = Quaternion.RotateTowards (sc.transform.rotation, rotation, speed*4 * Time.deltaTime);
		if (Quaternion.Angle (sc.transform.rotation, rotation) < 0.1f) {
			//					sc.GetComponent<Rigidbody> ().isKinematic = false;
//			sc.GetComponent<Rigidbody> ().isKinematic = false;
			sc.GetComponent<AISwordController> ().state = 3;
//			Debug.LogError ("");
		}
	}

	public void Substate5(GameObject sc, int i){
//		randomVector[i] = -AI.transform.forward;

//		Quaternion rotation = Quaternion.LookRotation (Vector3.Cross(AI.player.transform.forward, swords[i].transform.forward));
//		rotation *= Quaternion.FromToRotation(Vector3.forward, Vector3.right);
		swords[i].transform.rotation = Quaternion.RotateTowards (swords[i].transform.rotation, rotationTemp, speed*4 * Time.deltaTime);
		if (Quaternion.Angle (swords[i].transform.rotation, rotationTemp) < 1f) {
//			sc.GetComponent<Rigidbody> ().isKinematic = false;
//			sc.transform.LookAt (new Vector3(sc.transform.position.x, AI.player.transform.position.y, sc.transform.position.z));
			sc.GetComponent<AISwordController> ().state = 3;
		}

	}

	public void SubState5RotateZ(GameObject sc, int i){
		
	}
}
