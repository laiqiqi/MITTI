using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;
	public GameObject bullet;
	public Vector3 swordDirection;
	public Collision bodyColInfo;
	[HideInInspector] public AIState currentState;
	[HideInInspector] public FloatingAIState floatingState;
	[HideInInspector] public SeekState seekState;
    [HideInInspector] public StompState stompState;
	[HideInInspector] public PrepareDigStrikeState prepareDigStrikeState;
	[HideInInspector] public DigStrikeState digStrikeState;
	[HideInInspector] public ShootAIState shootState;
	[HideInInspector] public SlashState slashState;
	[HideInInspector] public ParryAIState parryState;
	[HideInInspector] public PrepareSlamState prepareSlamState;
	[HideInInspector] public SlamState slamState;
	[HideInInspector] public EscapeState escapeState;
	[HideInInspector] public bool isHit;
	[HideInInspector] public bool isParry;


	// Use this for initialization
	void Start () {
		floatingState = new FloatingAIState (this);
		seekState = new SeekState (this);
		stompState = new StompState (this);
		prepareDigStrikeState = new PrepareDigStrikeState (this);
		digStrikeState = new DigStrikeState (this);
		shootState = new ShootAIState (this);
		slashState = new SlashState (this);
		parryState = new ParryAIState (this);
		prepareSlamState = new PrepareSlamState (this);
		slamState = new SlamState (this);
		escapeState = new EscapeState (this);
		floatingState.StartState ();
		// seekState.StartState();
		swordDirection = Vector3.up;
		isHit = false;
		isParry = false;
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}

	void OnCollisionEnter(Collision coll){
//		Vector3 dir = coll.transform.position - transform.position;
//		coll.rigidbody.AddForce(dir.normalized * 500);
	}
}
