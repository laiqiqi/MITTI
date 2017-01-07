using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;
	public GameObject bullet;

	[HideInInspector] public AIState currentState;
	[HideInInspector] public FloatingAIState floatingState;
	[HideInInspector] public ShootAIState shootState;
//	[HideInInspector] public FloatingAIStateWithNav floatingStateWithNav;
	// Use this for initialization
	void Start () {
		floatingState = new FloatingAIState (this);
		shootState = new ShootAIState (this);
		floatingState.StartState ();

	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();

	}
}
