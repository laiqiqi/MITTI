using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UltiBulletCollision : MonoBehaviour {
	public int state;
	private float timeCount;
	private float timeToDestroy;
	void Start() {
		state = 0;
	}

	void Update(){
		Debug.Log ("state : " +state);
		if (state == 1) {
			timeCount = 0;
			timeToDestroy = 10f;
			state = 2;
		} else if (state == 2) {
//			this.transform.LookAt (player);
//			this.transform.position += this.transform.forward * 5f * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position, Player.instance.transform.position, 2f * Time.deltaTime);
			if (timeCount > timeToDestroy) {
				this.GetComponent<ParticleSystem> ().loop = false;
				if(!this.GetComponent<ParticleSystem>().IsAlive()){
					StatePatternAI.instance.effectManager.StopSoundByName (AISoundName.Ulti_SOUND);
					Destroy (this.gameObject);
					state = 0;
				}
			} else {
				timeCount += Time.deltaTime;
			}

		}
	}

	void OnTriggerEnter(Collider col) {
//		Debug.Log("Hit "+col.tag);
		if(col.tag.Equals("Player")){
			Player.instance.GetComponent<PlayerStat>().isStartDazzle = true;
			Destroy(this.gameObject);
		}
	}	
}
