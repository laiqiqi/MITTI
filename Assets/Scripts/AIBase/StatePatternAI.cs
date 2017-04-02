using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternAI: MonoBehaviour {
	public float health;
	public Transform target;
	public float speed;
	public GameObject player;
	public GameObject bullet;
	public GameObject body;
	public Collision bodyColInfo;
//-----------------------------Sword Components-------------------------------
	private FixedJoint swordJoint;
	public GameObject sword;
	public Vector3 swordDirection;
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
	[HideInInspector] public bool isHit;
	[HideInInspector] public bool isParry;
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
		swordJoint = this.GetComponent<FixedJoint>();

		awokenState = new AwokenState (this);
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
		stopState = new StopState(this);
		prepareSlashState = new PrepareSlashState (this);
		stunState = new StunState (this);


		swordDirection = Vector3.up;
		isHit = false;
		isParry = false;

		DetachSword();

		awokenState.choice.AddRange(new AIState[]{seekState});
		AIStateFlow.Add(awokenState, awokenState.choice);

		stopState.StartState();
		// awokenState.StartState();
		// floatingState.StartState();
		// seekState.StartState();
		// stompState.StartState();
		// prepareDigStrikeState.StartState();
		// prepareSlamState.StartState();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// KeyboardController();
		currentState.UpdateState();
		// KeyboardController();
		// Debug.Log(currentState.name);
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

	public void DetachSword(){
		Destroy(this.GetComponent<FixedJoint>());
		sword.gameObject.SetActive(false);
	}
	public void AttachSword(){
		sword.gameObject.SetActive(true);
		this.gameObject.AddComponent<FixedJoint>();
		this.gameObject.GetComponent<FixedJoint>().connectedBody = sword.GetComponent<Rigidbody>();
		this.gameObject.GetComponent<FixedJoint>().breakForce = Mathf.Infinity;
		this.gameObject.GetComponent<FixedJoint>().breakTorque = Mathf.Infinity;
		this.gameObject.GetComponent<FixedJoint>().enableCollision = false;
		this.gameObject.GetComponent<FixedJoint>().enablePreprocessing = true;
	}

	public Collision GetBodyCollisionInfo(){
		return bodyColInfo;
	}

	public void NextState(){
		Debug.Log(currentState == awokenState);
		Debug.Log(AIStateFlow[currentState].Count);
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
