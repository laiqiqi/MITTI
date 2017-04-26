using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootUltiState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	private float speed;
	private int subState;
	private Vector3 initialPosition;
	private float timeToShoot;
	private float timeCount;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}
	private GameObject Ulti;

	public ShootUltiState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		Debug.Log("aaaa=====");
		AI.currentState = AI.shootUltiState;
		speed = 200;
		subState = 0;
		timeToShoot = 5;
		timeCount = 0;
		AI.transform.LookAt (AI.player.transform);
		Ulti = GameObject.Instantiate (AI.UltiBullet,AI.transform.position+ AI.transform.forward * 2f, Quaternion.identity)as GameObject;
		// Ulti.GetComponent<UltiBulletCollision> ().player = AI.player.transform;
		AI.effectManager.PlaySoundByName (AISoundName.Ulti_SOUND);
	}

	public void UpdateState(){

		AI.transform.LookAt (AI.player.transform);
		if(timeCount > timeToShoot){
			Debug.Log(Time.deltaTime);
			Debug.Log(timeCount+" asasa");
			EndState ();
		}
		timeCount += Time.deltaTime;
	}

	public void EndState(){
		// Ulti.GetComponent<UltiBulletCollision> ().player = AI.player.transform;
		Ulti.GetComponent<UltiBulletCollision> ().state = 1;
//		AI.UltiBullet.GetComponent<UltiBulletCollision> ().state = 1;
//		AI.UltiBullet.GetComponent<UltiBulletCollision> ().player = AI.player.transform;
		AI.NextState ();
	}

	public void StateChangeCondition(){

	}
		


}
