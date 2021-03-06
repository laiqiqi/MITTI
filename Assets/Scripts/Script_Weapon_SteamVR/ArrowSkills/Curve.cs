﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Curve : MonoBehaviour {
	public float damage = 20.0f;
	private GameObject targetObj;
	private Transform target1;
	public float timeToLerp = 2;
	public float timeToDeath = 2.5f;
	private float timeLerped = 0.0f;
	public PhysicMaterial curveArrowMat;
	public ParticleSystem p;
	public PlaySound playHit;
	private Vector3 P0, P1, P2;
	// Use this for initialization
	void Start () {
		if(GameState.instance != null && GameState.instance.tutorialState || GameState.instance.mainGame){
			//check state  for target
			Debug.Log(GameState.instance.tutorialState +"TUTORIALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
			if(GameState.instance.tutorialState){
				// Debug.Log(GameState.instance.tutorialState +"Tageting tutorail AIIAIAIAAIAIAIAIAIAIAIA");
				// // targetObj  = FindObjectOfType<TutorialAI>().gameObject;
				//  targetObj  = FindObjectOfType<StatePatternAI>().gameObject;
				// target1 = targetObj.transform;
				Destroy(gameObject);
			}else if(GameState.instance.mainGame){
				Debug.Log("gamestate instance");
				targetObj = FindObjectOfType<StatePatternAI>().gameObject;
				target1 = targetObj.transform;
			}
			//random from the targets
			if(target1 != null){
				Transform[] targetPositions = targetObj.GetComponent<ExplodeTarget_Arrow>().targets;
				target1 = targetPositions[Random.Range(0,targetPositions.Length)];
				P0 = transform.position;
				P1 = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
				P2 = target1.position;
				StartCoroutine(death());
			}else{
				Destroy(gameObject);
			}

		}else{
			Debug.Log("Fucking delete godamn it");
			Destroy(gameObject);
		}
	}
	IEnumerator death(){
		yield return new WaitForSeconds(timeToDeath);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(target1 != null){
			P2 = target1.transform.position;
			timeLerped += Time.deltaTime;
			float t = timeLerped / timeToLerp;
			// Debug.Log("target1 transform " + P2 + " target object" + target1.root.gameObject);
			if (t >= 0 && t <= 1){
				transform.position = (1-t)*(1-t)*P0 + 2*(1-t)*t*P1 + t*t*P2;
				// Debug.Log("transform current pos" + transform.position);
				transform.LookAt(target1.transform);
			}
		}else{
			Debug.Log("Fucking delete godamn it");
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		Debug.LogWarning("hit on triiger enter" + col.gameObject.name + "targetObj" + targetObj );
		if(targetObj != null){
			Debug.Log("target obj: " + targetObj.name + "transform " + target1.gameObject);
			//check col 
			//play hit
			if(col.sharedMaterial == curveArrowMat){
				col.gameObject.SendMessageUpwards("ApplyDamage",damage, SendMessageOptions.DontRequireReceiver );
				p.Play();
				playHit.Play();
				Debug.Log(gameObject.transform.name + " hit object name: " + col.gameObject.name + " tag " + col.gameObject.tag );
				TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
				trail.time = 0;
				Rigidbody box = gameObject.GetComponent<Rigidbody>();
				box.transform.parent = col.gameObject.transform;
				box.velocity = Vector3.zero;
				box.angularVelocity = Vector3.zero;
				box.isKinematic = true;
				box.useGravity = false;
				box.transform.GetComponent<BoxCollider>().enabled = false;
			}
			//play hit sound
		}
		// Destroy(gameObject);
	}
	// void OnCollisionEnter(Collision collision){
	// 	if(collision.collider.gameObject.tag == "AI"){
	// 		collision.collider.gameObject.SendMessageUpwards( "ApplyDamage", damage, SendMessageOptions.DontRequireReceiver );
	// 		Rigidbody box = gameObject.GetComponent<Rigidbody>();
	// 		box.transform.parent = collision.collider.gameObject.transform;
	// 		box.velocity = Vector3.zero;
	// 		box.angularVelocity = Vector3.zero;
	// 		box.isKinematic = true;
	// 		box.useGravity = false;
	// 		box.transform.GetComponent<BoxCollider>().enabled = false;
	// 		StartCoroutine(fadeLineRenderer());
	// 		// Destroy(gameObject);
	// 	}
	// }

	IEnumerator fadeLineRenderer(){
		TrailRenderer g = this.transform.GetComponent<TrailRenderer>();
		Debug.Log(g.time + "gtime");
		while(g.time > 0){
			g.time = g.time - 0.4f;
			Debug.Log(g.time);
			if(g.time == 0){

				Destroy(gameObject);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
