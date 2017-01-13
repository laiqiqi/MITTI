using UnityEngine;
using System.Collections;

public class FloatingAIState : AIState {
	private readonly StatePatternAI enemy;
	public Transform target;
	public float speed;

	public FloatingAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		target = new GameObject ().transform;
	}

	public void StartState(){
		enemy.currentState = enemy.floatingState;
		speed = 10f;
//		target = new GameObject ().transform;
		target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
			//			this.transform.position.y + Random.Range (-10f, 100f),
			enemy.player.transform.position.y + Random.Range (0f, 10f),
			// Random.Range (0f, 10f),
			enemy.player.transform.position.z + Random.Range (-10f, 10f));
	}

	public void UpdateState(){
		Floating ();
		enemy.transform.LookAt(enemy.player.transform);
	}

	public void EndState(){
	
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
								enemy.player.transform.position.y + Random.Range (0f, 10f),
				// Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));
			enemy.shootState.StartState ();
			
		}
		if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < 4f){
			target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
				enemy.player.transform.position.y + Random.Range (0f, 10f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				// Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));

			enemy.slashState.StartState ();
		}
	}
}
