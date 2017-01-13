using UnityEngine;
using System.Collections;

public class SlashAIState : AIState {
	private readonly StatePatternAI enemy;
	public string name{get;}
	private float startYAngle;
	private int slashCount;
	private Vector3 oldSwordDirection;

	public SlashAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
	}

	public void StartState(){
		enemy.currentState = enemy.slashState;
		enemy.swordDirection = Mathf.Pow (-1, Random.Range (0, 2)) * enemy.swordDirection;
		oldSwordDirection = enemy.swordDirection;
		enemy.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
		startYAngle = enemy.transform.eulerAngles.y;
//		Debug.Log(enemy.swordDirection+"            "+Mathf.Abs (enemy.transform.eulerAngles.y));
		slashCount = 0;

	}

	public void UpdateState(){
		enemy.transform.Rotate (enemy.swordDirection * 300f * Time.deltaTime);
//		Transform swordTransform = enemy.transform.FindChild("Sword");
//		swordTransform.transform.localPosition = Vector3.Lerp(swordTransform.localPosition, new Vector3(2, 0, 2), 0.1f);

//		if (enemy.transform.rotation.y < 0f && enemy.transform.rotation.x < 0f) {
//			StateChangeCondition ();
//		}
		// Debug.Log(Mathf.Abs (enemy.transform.eulerAngles.y));
		if (enemy.swordDirection.y > 0) {
			if (Mathf.Abs (startYAngle - enemy.transform.eulerAngles.y) <= 20f// || Mathf.Abs (startYAngle - enemy.transform.eulerAngles.y) >= 355f
				&& startYAngle > enemy.transform.eulerAngles.y) {
				StateChangeCondition ();	
			}	
		} else {
			if (Mathf.Abs (startYAngle - enemy.transform.eulerAngles.y) <= 20f// || Mathf.Abs (startYAngle - enemy.transform.eulerAngles.y) >= 355f
				&& startYAngle < enemy.transform.eulerAngles.y) {
				StateChangeCondition ();	
			}
		}

		if (oldSwordDirection != enemy.swordDirection) {
			slashCount++;
		}
		if (slashCount > 2) {
			StateChangeCondition ();
		}
		oldSwordDirection = enemy.swordDirection;
//		if (Mathf.Abs (startYAngle - enemy.transform.eulerAngles.y) <= 5f && ) {
//			passFront = true;
//			Debug.Log ("passFront");
//		}
//		if (Mathf.Abs (enemy.transform.eulerAngles.y%360f) < 5f && passFront) {
//			StateChangeCondition ();			
//		}
	}

	public void EndState(){

	}

	public void StateChangeCondition(){
		enemy.floatingState.StartState ();
	}
}
