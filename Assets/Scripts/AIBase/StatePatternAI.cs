using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;
	public GameObject bullet;
	public GameObject body;
	public Vector3 swordDirection;
	public Collision bodyColInfo;
	public GameObject gObjEffectManager;
	[HideInInspector] public AIEffectManager effectManager;
	[HideInInspector] public AIState currentState;
	[HideInInspector] public FloatingAIState floatingState;
	[HideInInspector] public SeekState seekState;
    [HideInInspector] public StompState stompState;
	[HideInInspector] public PrepareDigStrikeState prepareDigStrikeState;
	[HideInInspector] public DigStrikeState digStrikeState;
	[HideInInspector] public ShootAIState shootState;
	[HideInInspector] public SlashAIState slashState;
	[HideInInspector] public ParryAIState parryState;
	[HideInInspector] public PrepareSlamState prepareSlamState;
	[HideInInspector] public SlamState slamState;

	// Use this for initialization
	void Start () {
		effectManager = gObjEffectManager.GetComponent<AIEffectManager>();
		floatingState = new FloatingAIState (this);
		seekState = new SeekState (this);
		stompState = new StompState (this);
		prepareDigStrikeState = new PrepareDigStrikeState (this);
		digStrikeState = new DigStrikeState (this);
		shootState = new ShootAIState (this);
		slashState = new SlashAIState (this);
		parryState = new ParryAIState (this);
		prepareSlamState = new PrepareSlamState (this);
		slamState = new SlamState (this);

		// floatingState.StartState ();
		seekState.StartState();

		// swordDirection = Vector3.up;

	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}

	void OnCollisionEnter (Collision coll) {
//		Vector3 dir = coll.transform.position - transform.position;
//		coll.rigidbody.AddForce(dir.normalized * 500);
	}

	public void ResetBody () {
		body.transform.position = new Vector3(0, 0, 0);
		body.transform.rotation = new Quaternion(0, 0, 0, 0);
		body.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
	}
}
