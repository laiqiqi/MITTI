using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageDetection : MonoBehaviour {
	private float health;
	private GameObject HealthScale;
	// private Slider slider;
	// Use this for initialization
	void Start () {
		health = 100f;
		// slider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
		HealthScale = GameObject.Find("HealthBarManager/EmptyScale");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision colli){
		// Debug.Log("hit by something");
		WeaponDetail d = colli.gameObject.GetComponent<WeaponDetail>();
		if(d != null){
			takeDamage(d.getWeaponDamage());
			if(health <= 0){
				Destroy(gameObject);
			}
			updateHealthBar(d.getWeaponDamage());
		}
	}

	private void updateHealthBar(float damage){
		HealthScale.transform.localScale -=  new Vector3(damage/100F,0,0) ;
	}
	void takeDamage(float damage){
		health -= damage;
		// Debug.Log(health);
	}
}
