using UnityEngine;
using System.Collections;

public class StatePatternAI: MonoBehaviour {
	public Transform target;
	public float speed;
	// public GameObject camRig;
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
	// public GameObject gObjAIManager;
	[HideInInspector] public AIEffectManager effectManager;
	[HideInInspector] public AIAnimationManager animationManager;
//----------------------------------------------------------------------------
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
	[HideInInspector] public bool isHit;
	[HideInInspector] public bool isParry;


	// Use this for initialization
	void Start () {
		// player = camRig.transform.Find("Camera (eye)").gameObject;
		effectManager = this.GetComponent<AIEffectManager>();
		animationManager = this.GetComponent<AIAnimationManager>();

		swordJoint = this.GetComponent<FixedJoint>();

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
		
		swordDirection = Vector3.up;
		isHit = false;
		isParry = false;

		DetachSword();

		// floatingState.StartState();
		seekState.StartState();
		// stompState.StartState();
		// prepareDigStrikeState.StartState();
		// prepareSlamState.StartState();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		KeyboardController();
		// Debug.Log("aaaaa");
		currentState.UpdateState();
		// KeyboardController();
		// Debug.Log(currentState.name);
	}

	public void ResetBody () {
		this.transform.rotation = new Quaternion(0, 0, 0, 0);
		body.transform.localPosition = new Vector3(0, 0, 0);
		body.transform.rotation = new Quaternion(0, 0, 0, 0);
		body.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
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
}
