using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

	// Use this for initialization
	public float stamina;
	public float staRegenRate;

	public float health;
	public float healthRegenRate;
	void Start () {
		stamina = 100;
		staRegenRate = 10;

		health = 100;
		healthRegenRate = 1;
	}
	
	// Update is called once per frame
	void Update () {
		StaminaRegen();
		Debug.Log("Stamina: " + stamina);
	}

	void StaminaRegen(){
		if(stamina < 100){
			if(stamina + (staRegenRate*Time.deltaTime) > 100){
				stamina = 100;
			}
			else{
				stamina += (staRegenRate*Time.deltaTime);
			}
		}
	}

	void Regen(float stat, float rate){
		if(stat < 100){
			if(stat + (rate*Time.deltaTime) < 100){
				stat = 100;
			}
			else{
				stat += (rate*Time.deltaTime);
			}
		}
	}
}
