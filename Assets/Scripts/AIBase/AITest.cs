using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour {
	public float speed;
	public GameObject player;
	public GameObject[] swordController;
	public float AIAndSwordDistance;
	[HideInInspector] public AIState currentState;
	[HideInInspector] public SwordFloatingAIState swordFloatingAIState;
	[HideInInspector] public SwordSlashingAIState swordSlashingAIState;
	[HideInInspector] public PrepareSlashingAIState prepareSlashingAIState;
	[HideInInspector] public SwordFindingAIState swordFindingAIState;
	[HideInInspector] public SwordPullingAIState swordPullingAIState;
	[HideInInspector] public SwordShootingAIState swordShootingAIState;

	// Use this for initialization
	void Start () {
		AIAndSwordDistance = 2f;

		swordFloatingAIState = new SwordFloatingAIState (this);
//		swordSlashingAIState = new SwordSlashingAIState (this);
		prepareSlashingAIState = new PrepareSlashingAIState (this);
//		swordFindingAIState = new SwordFindingAIState (this);
		swordPullingAIState = new SwordPullingAIState (this);
//		swordShootingAIState = new SwordShootingAIState (this);
//		this.swordSlashingAIState.StartState ();
		this.swordFindingAIState.StartState ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		currentState.UpdateState ();
	}
}
