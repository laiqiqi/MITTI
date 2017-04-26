using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingAIState : AIState {
	private readonly StatePatternAI enemy;
	public Transform target;
	public float speed;
	public string name{ get;}
	public List<AIState> choice{ get;set; }
	public float radiusOfMap = 18;
	public float stateDelay{ get;set;}

	public FloatingAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		target = new GameObject ().transform;
		name = "FloatingState";
		choice = new List<AIState>();
	}

	public void StartState(){
//		Debug.Log ("start   floating");
		enemy.currentState = enemy.floatingState;
		speed = 5f;
		if (enemy.isRage) {
			speed = 10f;
		}
		target.position = RandomPosition();
//		enemy.transform.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public void UpdateState(){
		// Debug.Log("floating");
		Floating ();
		enemy.transform.LookAt(enemy.player.transform);
//		enemy.GetComponent<Rigidbody> ().velsocity = Vector3.zero;
	}

	public void EndState(){
//		enemy.transform.GetComponent<Rigidbody> ().isKinematic = false;
		Debug.Log("enemy     AISword      "+ enemy.AISword.GetComponent<AISword>().state);
		if (enemy.AISword.GetComponent<AISword>().state == -2) {
//			Debug.Log ("asdlkfjalkdfjl;kasdjfkl;a");
			enemy.NextState ();
		}
	}

	public void StateChangeCondition(){
//		enemy.slashState.StartState ();
	}

	void Floating(){
		float step = speed * Time.deltaTime;
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.position, step);
		if(Vector3.Distance(enemy.transform.position, target.position) < 0.1f){
			//It is within ~0.1f range, do stuff
			target.position = RandomPosition();
			EndState ();
//			enemy.shootState.StartState ();
			// enemy.slashState.StartState ();
			
		}
		if(Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < 5f){
//			target.position = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
//				enemy.player.transform.position.y + Random.Range (1f, 2f),
//				enemy.player.transform.position.z + Random.Range (-10f, 10f));
			target.position = enemy.transform.position - enemy.transform.forward* 5f;

			// EndState ();

//			enemy.prepareSlashState.StartState ();
			// enemy.escapeState.StartState();
		}
	}

	Vector3 RandomPosition(){
		Vector3 pos = new Vector3 (enemy.player.transform.position.x + Random.Range (-10f, 10f),
			enemy.player.transform.position.y + Random.Range (0.5f, 1f),
			enemy.player.transform.position.z + Random.Range (-10f, 10f));

		if(Vector2.Distance(new Vector2(pos.x, pos.z), Vector2.zero) > radiusOfMap){
			pos.x = new Vector2 (pos.x, pos.z).normalized.x * radiusOfMap;
			pos.z = new Vector2 (pos.x, pos.z).normalized.y * radiusOfMap;
		}
		return pos;
	}
}
