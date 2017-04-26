using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	private float speed;
	private int subState;
	private List<GameObject> cubes;
	private float radius;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}
	private GameObject initialCube;
	private int numCube = 20;
	private Vector3 upPos;

	public DeadState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.deadState;
		speed = 200;
		subState = 0;
		radius = 2.5f;
		AI.magnet.GetComponent<ContinuousExplosionForce> ().force = 0.2f;
		upPos = AI.transform.position + new Vector3(0f, 5f, 0f);
		foreach (AISword child in GameObject.FindObjectsOfType<AISword> ()) {
			if (child.swordModel.GetComponent<FadeManager> ().isShow) {
				child.state = 6;
			}
			child.transform.parent = null;
			child.GetComponent<Rigidbody> ().useGravity = true;
		}
		GameObject.Destroy (AI.swordController[0].gameObject);
		foreach(ParticleSystem p in AI.body.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>()){
			p.loop = false;
		}
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		AI.effectManager.soundsDict [AISoundName.AI_SOUND].GetComponent<AudioSource>().volume -= 0.002f;
		if (subState == 0) {
			FloatUp ();
		} else if (subState == 1) {
//			foreach (Transform child in AI.magnet.transform) {
//				child.transform.parent = null;
//				child.GetComponent<Rigidbody> ().useGravity = true;
//			}
			foreach (AICube child in GameObject.FindObjectsOfType<AICube> ()) {
				child.transform.parent = null;
				child.GetComponent<Rigidbody> ().useGravity = true;
			}
			//		GameObject.find;
			GameObject.Destroy (AI.magnet);
//			foreach(ParticleSystem p in AI.body.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>()){
//				p.loop = false;
//			}
			subState = 2;
		}else if(subState == 2){
			int count = 0;
			foreach(ParticleSystem p in AI.body.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>()){
				if(p.IsAlive() == false){
					count++;
				}
			}
			if (count == AI.body.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>().Length) {
				GameObject.Destroy (AI.body.GetComponent<Animator>());
				AI.body.transform.parent = null;
				AI.body.GetComponent<Rigidbody> ().isKinematic = false;
				AI.body.GetComponent<Rigidbody> ().useGravity = true;
				subState = 3;
				EndState();
			}
		}else if(subState == 3){
			
		}

	}

	void FloatUp()
	{
		if(Vector3.Distance(AI.transform.position, upPos) > 0.1f){
			AI.transform.position = Vector3.MoveTowards(AI.transform.position, upPos, AI.speed/10 * Time.deltaTime);
		}
		else{
			subState = 1;
		}
	}

	public void EndState(){
		Debug.Log("EndOpen");
		GameState.instance.end = true;
//		AI.NextState();
	}

	public void StateChangeCondition(){

	}
}
