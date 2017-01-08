using UnityEngine;
using System.Collections;

public class FloatingAIStateWithNav : AIState {
	private readonly StatePatternAI enemy;
//	public Transform target;
	public float speed;

	public FloatingAIStateWithNav(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}

	public void StartState(){
		enemy.currentState = enemy.floatingState;
		speed = 10f;
//		target = new GameObject ().transform;
		enemy.target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
			//			this.transform.position.y + Random.Range (-10f, 100f),
			Random.Range (0f, 10f),
			enemy.player.transform.position.z + Random.Range (-10f, 10f));
	}

	public void UpdateState(){
		Floatiog ();
		enemy.transform.LookAt(enemy.player.transform);
	}

	public void EndState(){

	}

	public void StateChangeCondition(){
	}

	void Floatiog(){
		float step = speed * Time.deltaTime;
//		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.position, step);
		enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(enemy.target.position);
		if(Vector3.Distance(enemy.transform.position, enemy.target.position) < 5f){
			//It is within ~0.1f range, do stuff
			enemy.target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));
		}
		if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < 2f){
			enemy.target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				Random.Range (0f, 10f),
				enemy.player.transform.position.z + Random.Range (-10f, 10f));
		}
	}
}
