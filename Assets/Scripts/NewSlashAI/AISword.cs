using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AISword : MonoBehaviour {
	private float speed;
	public int state;
	public GameObject effect;
	public GameObject swordModel;
	public bool isHit;
	public bool isHitOther;
	private float timeCount;
	public bool virtualSword;
	public bool isHide;
	public PlaySound swordAppearSound;
	public PlaySound windSound;
	public PlaySound swordHitSound;
	public PlaySound swordSwipeSound;
	// Use this for initialization
	void Start () {
		speed = 300;
		isHit = false;
//		isHide = false;
		timeCount = 0;
//		virtualSword = false;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("Velocity    "+this.GetComponent<Rigidbody> ().velocity);
//		Debug.Log("Angular     "+this.GetComponent<Rigidbody> ().angularVelocity);
		if (state == 1) {
			this.transform.Rotate (0f, 0f, speed * Time.deltaTime);
		} else if (state == 2) {
			
		} else if (state == 0) {
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = this.GetComponent<Rigidbody> ().useGravity;
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().isKinematic = this.GetComponent<Rigidbody> ().isKinematic;
			if (this.transform.position.y < 4f && this.transform.position.y > 1f && !isHitOther) {
				RaycastHit hit;
				if (Physics.Raycast (this.transform.position, this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				} else if (Physics.Raycast (this.transform.position, -this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				} else if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 3.1f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				}
			}
		} else if (state == 3) {
			//While floating in air by shooting
			this.GetComponent<Rigidbody> ().AddForce (-this.transform.forward * 200f);
			this.GetComponent<MeleeWeaponTrail> ().Emit = true;
//			RaycastHit hit;
//			if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 3.1f)) {
//				print ("Ray    " + hit.transform.tag);
//				if (hit.transform.tag == "Ground") {
//			this.gameObject.layer = LayerMask.NameToLayer ("AISword");
//			this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");

		} else if (state == 4) {
			//Pierce through the ground
//			this.GetComponent<Rigidbody> ().isKinematic = true;
//			this.transform.position += -this.transform.forward*Time.deltaTime;
//			this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
		} else if (state == 5) {
			//effect out
//			Debug.Log("Effect out");
			foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
				p.loop = false;
//				p.Stop();
			}
			this.GetComponent<Rigidbody> ().useGravity = false;
			state = -1;
		} else if (state == 6) {
			//effect in
			Debug.Log ("effect in");
//			swordAppearSound.Play ();
			foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
				p.loop = true;
//				p.Play();
				Debug.Log ("loop       " + p.loop);
			}
			this.effect.GetComponent<PSMeshRendererUpdater> ().UpdateMeshEffect ();
			if (swordModel.GetComponent<FadeManager> ().isShow) {
				swordAppearSound.Play ();
				state = 7;
			} else {
				swordAppearSound.Play ();
				state = 8;
			}
		} else if (state == 7) {
			//sword out
			swordModel.GetComponent<FadeManager> ().Fade (-0.01f);
//			Debug.Log (swordModel.GetComponent<FadeManager> ().alpha);
			if (!swordModel.GetComponent<FadeManager> ().isShow) {
				state = 5;
			}
		} else if (state == 8) {
			//sword in
			swordModel.GetComponent<FadeManager> ().Fade (0.01f);
			if (swordModel.GetComponent<FadeManager> ().isShow) {
				state = 5;
			}
		} else if (state == -1) {
			if (!swordModel.GetComponent<FadeManager> ().isShow) {
				int count = 0;
				foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
					if(p.IsAlive() == false){
						count++;
					}
				}
				if (count == 2) {
					if (virtualSword) {
						Destroy (this.gameObject);
					} else {
						this.gameObject.SetActive (false);
					}
					state = -2;
					isHide =false;
				}
			}
			
		}

		if(isHide){
			if (timeCount >= 5) {
//				this.gameObject.SetActive (false);
				state = 6;
				isHide = false;
			}
			timeCount += Time.deltaTime;
		}
	}

	public void setHide(){
		swordModel.GetComponent<FadeManager> ().SetAlpha (0f);
		foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
			p.loop = false;

		}
		this.effect.GetComponent<PSMeshRendererUpdater> ().UpdateMeshEffect ();
	}
	void OnCollisionEnter(Collision other){
		if (other.transform.tag == "playsword") {
//			other.gameObject.GetComponent<PlayerStat> ().PlayerTakeDamage (10f);
			swordHitSound.Play ();
		} else if (other.transform.tag == "Player") {
			other.gameObject.GetComponent<PlayerStat> ().PlayerTakeDamage (10f);
		}
	}

	void OnCollisionStay(Collision other){
		if (other.transform.tag == "playsword") {
			isHit = true;
			if (state == 3) {
				this.GetComponent<Rigidbody> ().useGravity = true;
				this.GetComponent<Rigidbody> ().angularDrag = 0;
				state = 0;
			}
//			state = -1;
//			this.GetComponent<Rigidbody> ().useGravity = true;
		} else if(other.transform.tag == "SwordGround"){
			if (state == 0 || state == 4) {
				this.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}else{
//			Debug.Log ("other: name:"+other.transform.name+"   tag:"+other.transform.tag);
			isHitOther = true;
			if (state == 3 || state == 0) {
				windSound.Stop ();
				isHide = true;
				if (other.transform.tag == "Ground") {
					RaycastHit hit;
					if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 5f)) {
						if (hit.transform.tag == "Ground") {
							Debug.Log ("Sword 44444");
							state = 4;
							this.transform.position += -this.transform.forward * 2;
							this.GetComponent<Rigidbody> ().isKinematic = true;
						}
					}
				} else {
					this.GetComponent<Rigidbody> ().useGravity = true;
					state = 0;
				}
			}
		}
	}

	void OnCollisionExit(Collision other){
		if (other.transform.tag == "playsword") {
			isHit = false;
			this.transform.tag = "AISword";
			state = 10;
		}
		isHitOther = false;
		
	}
}
