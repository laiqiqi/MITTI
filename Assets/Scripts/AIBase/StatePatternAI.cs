using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternAI: MonoBehaviour {
	public float health;
	public float maxHealth;
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
	public GameObject UltiBullet;
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
	[HideInInspector] public PrepareSlamState prepareSlamState;
	[HideInInspector] public SlamState slamState;
	[HideInInspector] public EscapeState escapeState;
	[HideInInspector] public StopState stopState;
	[HideInInspector] public StunState stunState;

	[HideInInspector] public SwordSlashingAIState swordSlashingAIState;
	[HideInInspector] public SwordFindingAIState swordFindingAIState;
	[HideInInspector] public SwordShootingAIState swordShootingAIState;
	[HideInInspector] public OpeningState openingState;
	[HideInInspector] public ShootUltiState shootUltiState;
	[HideInInspector] public DeadState deadState;
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
		// health = 100f;
		effectManager = this.GetComponent<AIEffectManager>();
		animationManager = this.GetComponent<AIAnimationManager>();

		floatingState = new FloatingAIState (this);
		seekState = new SeekState (this);
		stompState = new StompState (this);
		prepareDigStrikeState = new PrepareDigStrikeState (this);
		digStrikeState = new DigStrikeState (this);
		prepareSlamState = new PrepareSlamState (this);
		slamState = new SlamState (this);
		escapeState = new EscapeState (this);
		stopState = new StopState(this);
		stunState = new StunState (this);
		swordSlashingAIState = new SwordSlashingAIState (this);
		swordFindingAIState = new SwordFindingAIState (this);
		swordShootingAIState = new SwordShootingAIState (this);
		openingState = new OpeningState (this);
		shootUltiState = new ShootUltiState (this);
		deadState = new DeadState (this);
//		isHit = false;
//		isParry = false;

//		openingState.choice.AddRange(new AIState[]{prepareSlamState});
		 openingState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(openingState, openingState.choice);

		seekState.choice.AddRange(new AIState[]{stompState});
		AIStateFlow.Add(seekState, seekState.choice);

		stompState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(stompState, stompState.choice);

		prepareDigStrikeState.choice.AddRange(new AIState[]{digStrikeState});
		AIStateFlow.Add(prepareDigStrikeState, prepareDigStrikeState.choice);

		digStrikeState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(digStrikeState, digStrikeState.choice);

		prepareSlamState.choice.AddRange(new AIState[]{slamState});
		AIStateFlow.Add(prepareSlamState, prepareSlamState.choice);

		slamState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(slamState, slamState.choice);

		escapeState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(escapeState, escapeState.choice);

		stopState.choice.AddRange(new AIState[]{});
		AIStateFlow.Add(stopState, stopState.choice);

		stunState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(stunState, stunState.choice);

		swordSlashingAIState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(swordSlashingAIState, swordSlashingAIState.choice);

		swordShootingAIState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(swordShootingAIState, swordShootingAIState.choice);

//		floatingState.choice.AddRange (new AIState[]{ floatingState, swordSlashingAIState, prepareSlamState
//		 											, seekState, prepareDigStrikeState, swordShootingAIState});
//		floatingState.choice.AddRange (new AIState[]{ floatingState, swordSlashingAIState, swordShootingAIState});
		floatingState.choice.AddRange (new AIState[]{ swordShootingAIState });
		AIStateFlow.Add(floatingState, floatingState.choice);

		shootUltiState.choice.AddRange(new AIState[]{floatingState});
		AIStateFlow.Add(shootUltiState, shootUltiState.choice);

		// foreach(AIState state in AIStateFlow.Keys){
		// 	Debug.Log(state);
		// }


//		shootUltiState.StartState ();
//		floatingState.StartState();
		openingState.StartState();
//		deadState.StartState();
		// swordSlashingAIState.StartState();
		// floatingState.StartState();
		// seekState.StartState();
		// stompState.StartState();
		// prepareDigStrikeState.StartState();
		// prepareSlamState.StartState();

//		stopState.StartState();

//		stopState.StartState();
		ChangeColorAI();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// KeyboardController();
//		Debug.Log(currentState);
		currentState.UpdateState();
//		 Debug.LogWarning (AIStateFlow[floatingState].Count);
		ChangeColorAI();
		// KeyboardController();
		// Debug.Log(currentState.name);
		// if(magnet.transform.parent == body.transform){
		// 	if(Vector3.Distance(magnet.transform.localPosition, Vector3.zero) > 0.1f){
		// 		magnet.transform.localPosition = Vector3.MoveTowards(magnet.transform.position, Vector3.zero, 5f * Time.fixedDeltaTime);
		// 	}
		// }
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
//			slashState.StartState();
		}else if (Input.GetKeyDown (KeyCode.O)) {
			currentState.EndState();
			stopState.StartState();
		}else if (Input.GetKeyUp (KeyCode.K)) {
		}

	}
	public void RagdollMode(){
        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().drag = 0;
        this.GetComponent<Rigidbody>().angularDrag = 0;
    }

    public void NoRagdollMode(){
        this.GetComponent<SphereCollider>().enabled = false;
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
		// if(health <= )
		if(currentState == floatingState){
			if (AIStateFlow [currentState].Count > 0) {
				int randomInt = Random.Range (0, AIStateFlow [currentState].Count);
				AIState randState = AIStateFlow [currentState] [randomInt];
				//			StartCoroutine (CooldownForState(100f, randState));
				if (randState != floatingState) {
					randState.StartState ();
					StartCoroutine (CooldownForState (20f, randState));
					AIStateFlow [floatingState].Remove (randState);
				}
			}
		}else if(AIStateFlow[currentState].Count == 1){
//			Debug.Log("Next1");
			Debug.Log(AIStateFlow[currentState][0]);
			AIStateFlow[currentState][0].StartState();
			// currentState = AIStateFlow[currentState][0];
		}
		else if(currentState == slamState){
//			Debug.Log("Next2");
			if(slamState.isStun){
				stunState.StartState();
			}
			else{
				AIStateFlow[currentState][0].StartState();
			}
		}
		// }else if(currentState == floatingState){
		// 	AIStateFlow[currentState][Random.Range(0, AIStateFlow[currentState].Count)].StartState();
		// }
		else{
//			Debug.Log("Next3");
//			Debug.Log(Random.Range(0, AIStateFlow[currentState].Count));
			int randomInt = Random.Range (0, AIStateFlow [currentState].Count);
			AIState randState = AIStateFlow [currentState] [randomInt];
//			StartCoroutine (CooldownForState(100f, randState));
			if (randState.name != floatingState.name) {
				randState.StartState();
				StartCoroutine (CooldownForState(20f, randState));
				AIStateFlow [floatingState].Remove (randState);
			}
		}
	}

	public void DisableMagnet() {
		magnet.GetComponent<ContinuousExplosionForce>().force = 0;
		magnet.GetComponent<ContinuousExplosionForce>().size = 0;
		// magnet.transform.parent = null;
	}

	public void EditMagnet(float radius, int size) {
		magnet.GetComponent<ContinuousExplosionForce>().radius = radius;
		magnet.GetComponent<ContinuousExplosionForce>().size = size;
		// magnet.transform.parent = body.transform;
		// magnet.transform.localPosition = Vector3.zero;	
	}

	IEnumerator CooldownForState(float waitTime, AIState state){
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("add state");
//		floatingState.choice.Add (state);
		AIStateFlow[floatingState].Add(state);

	}

	void ChangeColorAI(){
		GameObject fog = body.transform.GetChild (0).transform.GetChild (2).gameObject;
		ParticleSystem ps = fog.GetComponent<ParticleSystem>();
		float h, s, v;
		Color.RGBToHSV (ps.startColor, out h, out s, out v);
		v = health/100f;
//		s = maxHealth/100f;
		s -= 0.01f;
		ps.startColor = Color.HSVToRGB (h, s, v);

	}

	void ChangeColorByDamage(){
		GameObject fog = body.transform.GetChild (0).transform.GetChild (2).gameObject;
		ParticleSystem ps = fog.GetComponent<ParticleSystem>();
		float h, s, v;
		Color.RGBToHSV (ps.startColor, out h, out s, out v);
		s = 100f;
		ps.startColor = Color.HSVToRGB (h, s, v);
	}
}
