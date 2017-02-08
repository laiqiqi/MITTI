using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageDetection : MonoBehaviour {
	private float health;

	private Slider slider;
	// Use this for initialization
	void Start () {
		health = 100f;
		slider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision colli){
		Debug.Log("hit by something");
		WeaponDetail d = colli.gameObject.GetComponent<WeaponDetail>();
		if(d != null){
			takeDamage(d.getWeaponDamage());
			if(health <= 0){
				Destroy(gameObject);
			}
			updateHealthBar();
		}
	}

	private void updateHealthBar(){
		slider.value = health;
	}
	void takeDamage(float damage){
		health -= damage;
		Debug.Log(health);
	}
}
