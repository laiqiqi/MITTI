using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AICube : MonoBehaviour {
	private float speed;
	public int state;
	public DamageDetection AIDamage;
	public FireSource AIFire;
	public GameObject effect;
	public GameObject model;
	public bool isHit;
	public bool isHitOther;
	private float timeCount;
	public bool virtualSword;
	public bool isHide;
	// Use this for initialization
	void Start () {
		speed = 300;
		isHit = false;
		//		isHide = false;
		timeCount = 0;
		//		virtualSword = false;
		gameObject.layer = 9;
		effect.layer = 9;
		model.layer = 9;
	}

	// Update is called once per frame
	void Update () {
		//		Debug.Log("Velocity    "+this.GetComponent<Rigidbody> ().velocity);
		//		Debug.Log("Angular     "+this.GetComponent<Rigidbody> ().angularVelocity);
		if (state == 5) {
			//effect out
			//			Debug.Log("Effect out");
			foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
				p.loop = false;
				//				p.Stop();
			}
			state = -1;
		} else if (state == 6) {
			//effect in
			Debug.Log ("effect in");
			foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
				p.loop = true;
				//				p.Play();
				// Debug.Log ("loop       " + p.loop);
			}
			this.effect.GetComponent<PSMeshRendererUpdater> ().UpdateMeshEffect ();
			if (model.GetComponent<FadeManager> ().isShow) {
				state = 7;
			} else {
				state = 8;
			}
		} else if (state == 7) {
			//sword out
			model.GetComponent<FadeManager> ().Fade (-0.01f);
			//			Debug.Log (model.GetComponent<FadeManager> ().alpha);
			if (!model.GetComponent<FadeManager> ().isShow) {
				state = 5;
			}
		} else if (state == 8) {
			//sword in
			// Debug.LogError("aaaaaaaaaaaaaaaaaaa");
			model.GetComponent<FadeManager> ().Fade (0.05f);
			if (model.GetComponent<FadeManager> ().isShow) {
				state = 5;
			}
		} else if (state == -1) {
//			if (!model.GetComponent<FadeManager> ().isShow) {
				int count = 0;
				foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
					if(p.IsAlive() == false){
						count++;
					}
				}
			// Debug.Log (this.effect.transform.GetComponentsInChildren<ParticleSystem>().Length+"      length");
			if (count == this.effect.transform.GetComponentsInChildren<ParticleSystem>().Length) {
				// Debug.Log (this.effect.transform.GetComponentsInChildren<ParticleSystem>().Length+"      Hide");
					isHide = true;
					state = 0;
				}
//			}

		}
	}

	public void setHide(){
		model.GetComponent<FadeManager> ().SetAlpha (0f);
		foreach (ParticleSystem p in this.effect.transform.GetComponentsInChildren<ParticleSystem>()) {
			p.loop = false;

		}
		// this.effect.GetComponent<PSMeshRendererUpdater> ().UpdateMeshEffect ();
	}

	// void ApplyDamage(float dmg){
	// 	Debug.Log(" Bow arrow collision enter " + gameObject.name);
	// 	AIDamage.HitMagnetDmg(dmg);
	// }

	private void FireExposure()
		{
			AIFire.gameObject.SendMessage( "FireExposure", gameObject, SendMessageOptions.DontRequireReceiver );
		}
		
}
