using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageDetection : MonoBehaviour {
	// private float maxhealth;
	private float startHealth;
	public float maxHealth = 30000f;
	public Text texthealth;
	// public GameObject healthBar;
	void Start () {
		startHealth = maxHealth;
		StatePatternAI.instance.health = startHealth;
		StatePatternAI.instance.maxHealth = maxHealth;
		// maxhealth = StatePatternAI.instance.health;
		// slider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
	}
	
	void Update () {
		// Debug.Log("AI Health: " + StatePatternAI.instance.health);
		// if(healthBar!=null){
		// 	// Debug.Log("Set!" + StatePatternAI.instance.health);
		// 	healthBar.transform.localScale = new Vector3(StatePatternAI.instance.health / maxhealth, 1, 1);
		// }	
	}
	void ApplyDamage(float dmg){
		if(startHealth - dmg <= 0) {
			 startHealth = 0;
		}else{
			startHealth -= dmg;
		} 
		StatePatternAI.instance.health = startHealth;
		StatePatternAI.instance.ChangeColorByDamage ();
		// if(startHealth - dmg <= 0) {
		// 	 startHealth = maxHealth;
		// }else{
		// 	startHealth -= dmg;
		// } 
		// StatePatternAI.instance.health = startHealth;
		// WriteText();


		WriteText();
	}

	void WriteText(){
		texthealth.text = startHealth.ToString();
	}

	public void HitMagnetDmg(float dmg){
		ApplyDamage(dmg/2f);
	}

	void OnTriggerEnter(Collider col){

	}


}
