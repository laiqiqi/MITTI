using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;

	[HideInInspector] public AIState currentState;
	[HideInInspector] public FloatingAIState floatingState;
	// Use this for initialization
	void Start () {
		floatingState = new FloatingAIState (this);
		floatingState.StartState ();

	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();

	}
}
