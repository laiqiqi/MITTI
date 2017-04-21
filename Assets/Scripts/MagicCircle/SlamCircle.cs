using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamCircle : MonoBehaviour {
	private float health;
	[HideInInspector] public bool isBreak;
	private GameObject circle;
	private Color hitColor, normalColor;
	private Collider circleCollider;
	public GameObject slamBreak;
	// Use this for initialization
	void Start () {
		health = 100f;
		isBreak = false;

		circle = this.transform.GetChild(0).gameObject;
		circleCollider = this.GetComponent<Collider>();
		circleCollider.enabled = false;

		normalColor = circle.GetComponent<Renderer>().material.GetColor("_TintColor");
		hitColor = new Color(normalColor.r, normalColor.g / 1.75f, normalColor.b / 1.75f, 1f);	
	}
	
	// Update is called once per frame
	void Update () {
		if(circle.GetComponent<SelfRotate>().isOkColor){
			circleCollider.enabled = true;
		}

		if(health <= 0f) {
			isBreak = true;
			slamBreak.SetActive(true);
			circle.SetActive(false);
			this.transform.SetParent(null);
		}

		CircleColorRecover();
		this.transform.LookAt(StatePatternAI.instance.player.transform);
	}

	void OnTriggerEnter(Collider col){
		// Debug.Log("Hit "+col);
		if(col.tag == "PlayerWeapon"){
			Debug.Log("HitSlamCircle");
			circle.GetComponent<Renderer>().material.SetColor("_TintColor", hitColor);
			health -= 30f;
		}
	}

	void CircleColorRecover(){
		Color nowColor = circle.GetComponent<Renderer>().material.GetColor("_TintColor");
		if(nowColor.g < normalColor.g){
			nowColor.g += 0.3f * Time.deltaTime;
			nowColor.b += 0.3f * Time.deltaTime;
			circle.GetComponent<Renderer>().material.SetColor("_TintColor", nowColor);
		}
	}
}
