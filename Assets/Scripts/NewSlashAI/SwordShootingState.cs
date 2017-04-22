using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordShootingAIState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	private float speed;
	private int subState;
	private List<GameObject> swordList;
	private float radius;
	private GameObject initialSword;
	private int swordQuantity;
	private Vector3 initialPosition;
	private float timeToShoot;
	private float delayToShoot;
	public List<AIState> choice{ get;set; }

	public SwordShootingAIState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		Debug.Log("aaaa=====");
		AI.currentState = AI.swordShootingAIState;
		speed = 200;
		subState = 0;
		radius = 2.5f;
//		sc.GetComponent<AISwordController> ().state = 0;
		AI.swordController[0].transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
		swordList = new List<GameObject> ();
//		initialSword = AI.swordController [0].transform.GetChild (0).gameObject;
		AI.AISword.transform.parent = AI.swordController[0].transform;
		AI.AISword.gameObject.SetActive(false);
		AI.AISword.GetComponent<Rigidbody> ().isKinematic = true;
		AI.AISword.transform.localPosition = new Vector3 (0f, 0f, 1f);
		AI.AISword.transform.rotation = Quaternion.Euler (new Vector3 (90f, 0f, 0f));
//		AI.AISword.gameObject.SetActive(true);
		initialSword = AI.AISword.gameObject;

//		initialSword.SetActive (true);
		initialSword.GetComponent<AISword> ().setHide ();
		swordQuantity = 6;
		timeToShoot = 0;
		delayToShoot = 1;
//		swordQuantity = Random.Range (2, 7);
//		initialSword.transform.parent = null;
//		GameObject test = GameObject.Instantiate (AI.swordController[0].transform.GetChild(0).gameObject,Vector3.zero, Quaternion.identity) as GameObject;
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		if (subState == 0) {
			// Debug.Log ("000000000000");
			 SubState0_2 ();
		} else if (subState == 1) {
			// Debug.Log ("111111111111");
			SubState1 ();
//			foreach (GameObject sword in swordList) {

//			for(int i = swordList.Count-1; i >=0; i--){
//				swordList[i].transform.LookAt (2 * swordList[i].transform.position - AI.player.transform.position);
//				Vector3 pos1 = swordList[i].transform.position - AI.transform.position;
//				Vector3 pos2 = AI.transform.up*-radius;
//				Debug.Log ("pos1    "+pos1);
//				Debug.Log ("pos2    "+pos2);
//				if (i == swordList.Count-1 && 360f / (swordQuantity) - Vector3.Angle (pos1, pos2) < 0.5f) {
//					if (swordList.Count < swordQuantity) {
//						Vector3 relativePos = AI.transform.forward;
//						Quaternion rotation = Quaternion.LookRotation (relativePos);
//						GameObject newSword = GameObject.Instantiate (initialSword, AI.transform.position + AI.transform.up * -radius, Quaternion.identity) as GameObject;
//						newSword.transform.rotation = rotation;
//						newSword.transform.parent = AI.swordController [0].transform;
//						swordList.Add (newSword);
//						Debug.Log ("Angle      "+Vector3.Angle(pos1, pos2));
//					}
//				}
//			}
		} else if (subState == 2) {
//			AI.swordController [0].transform.Rotate (Vector3.up);
//			AI.swordController [0].transform.LookAt (AI.player.transform);
			Vector3 relativePos = AI.transform.up;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
//			AI.swordController [0].transform.rotation = Quaternion.RotateTowards (AI.swordController [0].transform.rotation, rotation, speed/2f * Time.deltaTime);
			AI.swordController [0].transform.rotation = rotation;
//			if (Quaternion.Angle (AI.swordController [0].transform.rotation, rotation) < 1f) {
				subState = 3;
				initialPosition = initialSword.transform.position;
//			}
		} else if (subState == 3) {
			int count = 0;
			AI.swordController [0].transform.Rotate (Vector3.up);
			AI.transform.position += AI.transform.up * 2*Time.deltaTime;
			foreach (GameObject sword in swordList) {
				sword.transform.LookAt (2 * sword.transform.position - AI.player.transform.position);
				if (Mathf.Abs (sword.transform.position.y - AI.transform.position.y) < 1) {
//					if(!sword.GetComponent<AISword>().swordModel.GetComponent<FadeManager>().isShow){
					if (sword.activeSelf == false) {
						sword.SetActive (true);
						sword.GetComponent<AISword> ().state = 6;
					}
//					if (sword.GetComponent<AISword> ().swordModel.GetComponent<FadeManager> ().isShow) {
//						sword.GetComponent<AISword> ().state = -1;
//					}
//					}
				}
				if(sword.GetComponent<AISword>().swordModel.GetComponent<FadeManager>().isShow) {
					count++;
				}
			}
				
			if(timeToShoot > 4f || count == swordList.Count){
				subState = 4;
				timeToShoot = 0;
			}
			timeToShoot += Time.deltaTime;
		} else if (subState == 4){
//			Debug.Log ("444444444444444");
			if (swordList.Count != 0) {
				foreach (GameObject sword in swordList) {
					sword.transform.LookAt (2 * sword.transform.position - AI.player.transform.position);	
				}
				Debug.Log ("arrayyyy");
				if (timeToShoot > delayToShoot) {
					Debug.Log ("shoot");
					int randomIndex = Random.Range (0, swordList.Count);
					swordList [randomIndex].GetComponent<Rigidbody> ().isKinematic = false;
					swordList [randomIndex].transform.parent = null;
					swordList [randomIndex].GetComponent<AISword> ().state = 3;
//					swordList [randomIndex].GetComponent<Rigidbody> ().AddForce (-swordList[randomIndex].transform.forward*500f);
					swordList.RemoveAt (randomIndex);
					timeToShoot = 0;
					delayToShoot /= 2f;
					if (swordList.Count == 0) {
						AI.NextState ();
					}
				}
			}
			timeToShoot += Time.deltaTime;
		}
	}

	public void EndState(){
		
	}

	public void StateChangeCondition(){

	}

	public void SubState0(){
		Vector3 relativePos = AI.transform.forward;
		Quaternion rotation = Quaternion.LookRotation (relativePos);

//		rotation = Quaternion.Euler (rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
		initialSword.transform.rotation = Quaternion.RotateTowards (initialSword.transform.rotation, rotation, speed * Time.deltaTime);

//		float distance = radius;
//		Vector3 target = -AI.transform.up.normalized * distance;
//		Vector3 target = AI.transform.position +new Vector3(0f, -5f*distance, 0f);
		Vector3 target = AI.transform.position + AI.transform.up*-radius;
		initialSword.transform.position = Vector3.MoveTowards (initialSword.transform.position, target, speed / 50f * Time.deltaTime);

//		Debug.Log ("Vector   "+Vector3.Distance (initialSword.transform.localPosition, target));
//		Debug.Log ("Quater   "+Quaternion.Angle (initialSword.transform.rotation, rotation));
		if (Vector3.Distance (initialSword.transform.position, target) < 1f
			&& Quaternion.Angle (initialSword.transform.rotation, rotation) < 1f) {
			subState = 1;
			swordList.Add(initialSword);
			AI.swordController [0].transform.rotation = AI.transform.rotation;
			initialSword.transform.parent = AI.swordController [0].transform;
//			initialSword.GetComponent<SwordFloatingSword>().state = 3;
		}
	}

	public void SubState0_2(){
//		Vector3 target = AI.transform.position + AI.transform.forward*-radius;
		Vector3 target = GetCircle(AI.transform.position, radius, 0);
//		initialSword.transform.position = Vector3.MoveTowards (initialSword.transform.position, target, speed / 50f * Time.deltaTime);
		initialSword.transform.position = target;
//		if (Vector3.Distance (initialSword.transform.position, target) < 0.1f){
			subState = 1;
			swordList.Add(initialSword);
//			AI.swordController [0].transform.rotation = AI.transform.rotation;
			initialSword.transform.parent = AI.swordController [0].transform;
			//			initialSword.GetComponent<SwordFloatingSword>().state = 3;
//		}
	}

	public void SubState1(){
		for(int i = 1; i < swordQuantity; i++){
			Vector3 pos = GetCircle (AI.transform.position, radius, i * (360f / swordQuantity));

			Vector3 relativePos = -AI.transform.up;
			Quaternion rotation = Quaternion.LookRotation (relativePos);

			GameObject newSword = GameObject.Instantiate (initialSword, pos, rotation) as GameObject;
//			newSword.transform.rotation = rotation;
			newSword.transform.parent = AI.swordController [0].transform;
			newSword.GetComponent<AISword> ().virtualSword = true;
//			newSword.SetActive (false);
			swordList.Add (newSword);
//			Debug.Log ("Angle      "+Vector3.Angle(pos1, pos2));
		}
		subState = 2;
	}

	Vector3 GetCircle(Vector3 center, float radius,float a){
		float ang = a;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}
}
