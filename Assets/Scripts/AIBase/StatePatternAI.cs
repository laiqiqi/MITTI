using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternAI: MonoBehaviour {
	public float health;
	// public Transform target;
	public float speed;
	public float agile;
	public bool isRage;
	public GameObject player;
	public GameObject bullet;
	public GameObject body;
	public Collision bodyColInfo;
	public GameObject magnet;
	public GameObject[] swordController;
	public GameObject AISword;
	public GameObject AICube;
//-----------------------------Sword Components-------------------------------
	
//----------------------------------------------------------------------------

//-----------------------------AI Manager-------------------------------------
	[HideInInspector] public AIEffectManager effectManager;
	[HideInInspector] public AIAnimationManager animationManager;
//----------------------------------------------------------------------------

//-----------------------------State List-------------------------------------
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
	[HideInInspector] public StopState stopState;
	[HideInInspector] public PrepareSlashState prepareSlashState;
	[HideInInspector] public StunState stunState;
	[HideInInspector] public AwokenState awokenState;

//	[HideInInspector] public SwordFloatingAIState swordFloatingAIState;
	[HideInInspector] public SwordSlashingAIState swordSlashingAIState;
//	[HideInInspector] public PrepareSlashingAIState prepareSlashingAIState;
	[HideInInspector] public SwordFindingAIState swordFindingAIState;
//	[HideInInspector] public SwordPullingAIState swordPullingAIState;
	[HideInInspector] public SwordShootingAIState swordShootingAIState;
	[HideInInspector] public OpeningState openingState;
//	[HideInInspector] public bool isHit;
//	[HideInInspector] public bool isParry;
//----------------------------------------------------------------------------

	//-------------------------------------------------
	// Singleton instance of the FirstAI. Only one can exist at a time.
	//-------------------------------------------------
	private static StatePatternAI _instance;
	public static StatePatternAI instance
	{
		get
		{
			if ( _instance == null )
			{
				_instance = FindObjectOfType<StatePatternAI>();
			}
			return _instance;
		}
	}
	public Dictionary<AIState, List<AIState>> AIStateFlow = new Dictionary<AIState, List<AIState>>();

	// Use this for initialization
	void Start () {
		health = 100f;
		effectManager = this.GetComponent<AIEffectManager>();
		animationManager = this.GetComponent<AIAnimationManager>();

		awokenState = new AwokenState (this);
		floatingState = new FloatingAIState (this);
		seekState = new SeekState (this);
		stompState = new StompState (this);
		prepareDigStrikeState = new PrepareDigStrikeState (this);
		digStrikeState = new DigStrikeState (this);
		shootState = new ShootAIState (this);
		// slashState = new SlashState (this);
		parryState = new ParryAIState (this);
		prepareSlamState = new PrepareSlamState (this);
		slamState = new SlamState (this);
		escapeState = new EscapeState (this);
		stopState = new StopState(this);
		prepareSlashState = new PrepareSlashState (this);
		stunState = new StunState (this);
//		swordFloatingAIState = new SwordFloatingAIState (this);
		swordSlashingAIState = new SwordSlashingAIState (this);
//		prepareSlashingAIState = new PrepareSlashingAIState (this);
		swordFindingAIState = new SwordFindingAIState (this);
//		swordPullingAIState = new SwordPullingAIState (this);
		swordShootingAIState = new SwordShootingAIState (this);
		openingState = new OpeningState (this);

//		isHit = false;
//		isParry = false;

		awokenState.choice.AddRange(new AIState[]{seekState});
		AIStateFlow.Add(awokenState, awokenState.choice);

		floatingState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(floatingState, floatingState.choice);

		seekState.choice.AddRange(new AIState[]{stompState});
		AIStateFlow.Add(seekState, seekState.choice);

		stompState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(stompState, stompState.choice);

		prepareDigStrikeState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(prepareDigStrikeState, prepareDigStrikeState.choice);

		digStrikeState.choice.AddRange(new AIState[]{seekState});
		AIStateFlow.Add(digStrikeState, digStrikeState.choice);

		shootState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(shootState, shootState.choice);

		prepareSlamState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(prepareSlamState, prepareSlamState.choice);

		slamState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(slamState, slamState.choice);

		escapeState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(escapeState, escapeState.choice);

		stopState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(stopState, stopState.choice);

		stunState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(stunState, stunState.choice);

		foreach(AIState state in AIStateFlow.Keys){
			Debug.Log(state);
		}


//		floatingState.StartState();
		openingState.StartState();
		swordSlashingAIState.StartState();
		// floatingState.StartState();
		// seekState.StartState();
		// stompState.StartState();
		// prepareDigStrikeState.StartState();
		// prepareSlamState.StartState();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// KeyboardController();
//		Debug.Log(currentState);
		currentState.UpdateState();
		// KeyboardController();
		// Debug.Log(currentState.name);
	}

	void InitAllState(){

	}

	public void ResetBody () {
		body.transform.localPosition = new Vector3(0, 0, 0);
		body.transform.localRotation = Quaternion.Euler(0, 0, 0);
		body.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
		this.transform.rotation = new Quaternion(0, 0, 0, 0);
	}

	void KeyboardController(){
		if (Input.GetKeyDown (KeyCode.I)) {
			currentState.EndState();
			slashState.StartState();
		}else if (Input.GetKeyDown (KeyCode.O)) {
			currentState.EndState();
			stopState.StartState();
		}else if (Input.GetKeyUp (KeyCode.K)) {
		}

	}
	public void RagdollMode(){
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().drag = 0;
        this.GetComponent<Rigidbody>().angularDrag = 0;
    }

    public void NoRagdollMode(){
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().drag = Mathf.Infinity;
        this.GetComponent<Rigidbody>().angularDrag = Mathf.Infinity;
    }

	public Collision GetBodyCollisionInfo(){
		return bodyColInfo;
	}

	public void NextState(){
		// Debug.Log(currentState == awokenState);
		// Debug.Log(AIStateFlow[currentState].Count);
		if(AIStateFlow[currentState].Count == 1){
			Debug.Log("Next1");
			Debug.Log(AIStateFlow[currentState][0]);
			AIStateFlow[currentState][0].StartState();
			// currentState = AIStateFlow[currentState][0];
		}
		else if(currentState == slamState){
			Debug.Log("Next2");
			if(slamState.isStun){
				currentState = stunState;
			}
			else{

			}
		}
		else{
			Debug.Log("Next3");
			currentState = AIStateFlow[currentState][Random.Range(0, AIStateFlow[currentState].Count)];
		}
	}
}
