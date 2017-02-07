using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour {
	public float speed;
	public GameObject player;
	public GameObject[] swordController;
	[HideInInspector] public AIState currentState;
	[HideInInspector] public SwordFloatingAIState swordFloatingAIState;
	[HideInInspector] public SwordSlashingAIState swordSlashingAIState;
	[HideInInspector] public PrepareSlashingAIState prepareSlashingAIState;

	// Use this for initialization
	void Start () {
		swordFloatingAIState = new SwordFloatingAIState (this);
		swordSlashingAIState = new SwordSlashingAIState (this);
		prepareSlashingAIState = new PrepareSlashingAIState (this);
//		this.swordSlashingAIState.StartState ();
		this.swordFloatingAIState.StartState ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		currentState.UpdateState ();
	}
}
