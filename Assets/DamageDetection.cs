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
		Debug.Log("damage applies");
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

	void FireExposureDamage(float dmg){
		Debug.Log("appplr fire");
		StartCoroutine(BurnDamage(dmg, 8f));
	}
	public void HitMagnetDmg(float dmg){
		ApplyDamage(dmg/2f);
	}
	IEnumerator BurnDamage(float dmg, float sec){
		float count = 0;
		while(count <= sec){
			count++;
			ApplyDamage(dmg/sec);
			yield return new WaitForSeconds(1f);
		}
	}
	void OnTriggerEnter(Collider col){

	}


}
