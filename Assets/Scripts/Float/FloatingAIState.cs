using UnityEngine;
using System.Collections;

public class FloatingAIState : AIState {
	private readonly StatePatternAI enemy;
	public Transform target;
	public float speed;
	public string name{ get;}

	public FloatingAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		target = new GameObject ().transform;
		name = "FloatingState";
	}

	public void StartState(){
		enemy.currentState = enemy.floatingState;
		enemy.DetachSword();
		speed = 5f;
//		target = new GameObject ().transform;
		target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
			//			this.transform.position.y + Random.Range (-10f, 100f),
			enemy.player.transform.position.y + Random.Range (0f, 2f),
			// Random.Range (0f, 10f),
			enemy.player.transform.position.z + Random.Range (-10f, 10f));
//		enemy.transform.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public void UpdateState(){
		Floating ();
		enemy.transform.LookAt(enemy.player.transform);
//		enemy.GetComponent<Rigidbody> ().velsocity = Vector3.zero;
	}

	public void EndState(){
//		enemy.transform.GetComponent<Rigidbody> ().isKinematic = false;
	}

	public void StateChangeCondition(){
		enemy.slashState.StartState ();
	}

	void Floating(){
		float step = speed * Time.deltaTime;
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.position, step);
		if(Vector3.Distance(enemy.transform.position, target.position) < 0.1f){
			//It is within ~0.1f range, do stuff
			target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
								enemy.player.transform.position.y + Random.Range (1f, 2f),
				// Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));
			EndState ();
			enemy.shootState.StartState ();
			// enemy.slashState.StartState ();
			
		}
		if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < 3f){
			target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
				enemy.player.transform.position.y + Random.Range (1f, 2f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				// Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));
			EndState ();
			enemy.prepareSlashState.StartState ();
			// enemy.escapeState.StartState();
		}
	}
}
