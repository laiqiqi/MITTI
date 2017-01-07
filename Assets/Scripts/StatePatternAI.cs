using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;
	public GameObject bullet;

	[HideInInspector] public AIState currentState;
	[HideInInspector] public FloatingAIState floatingState;
	[HideInInspector] public SeekState seekState;
    [HideInInspector] public StompState stompState;
	[HideInInspector] public PrepareDigStrikeState prepareDigStrikeState;
	[HideInInspector] public DigStrikeState digStrikeState;
	[HideInInspector] public ShootAIState shootState;

	// Use this for initialization
	void Start () {
		floatingState = new FloatingAIState (this);
		seekState = new SeekState (this);
		stompState = new StompState (this);
		prepareDigStrikeState = new PrepareDigStrikeState (this);
		digStrikeState = new DigStrikeState (this);
		// floatingState.StartState ();
		seekState.StartState();
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}
}
