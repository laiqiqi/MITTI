using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageDetection : MonoBehaviour {
	private float maxhealth;

	public GameObject healthBar;
	void Start () {
		maxhealth = StatePatternAI.instance.health;
		// slider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
	}
	
	void Update () {
		// Debug.Log("AI Health: " + StatePatternAI.instance.health);
		if(healthBar!=null){
			// Debug.Log("Set!" + StatePatternAI.instance.health);
			healthBar.transform.localScale = new Vector3(StatePatternAI.instance.health / maxhealth, 1, 1);
		}	
	}
	void ApplyDamage(float dmg){
		if(StatePatternAI.instance.health - dmg > 0){
			StatePatternAI.instance.health -= dmg;
		}
	}


}
