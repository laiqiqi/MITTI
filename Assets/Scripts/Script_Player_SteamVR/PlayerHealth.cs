
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {

	// Use this for initialization
	public float fullhealth = 100f;
	private float health;
	public Scrollbar Scrollbar;
	void Start () {
		health = fullhealth;
	}

	public void ApplyHealing(float amount){
		if(health + amount >= fullhealth){
			health = fullhealth;
		}else{
			health += amount;
		}
	}
	public void ApplyDamage(float amount){
		if(health - amount <= 0){
			health = 0;
		}else{
			health -= amount;
		}
	}

	// void OnTriggerEnter(Collider col){
	// 	if (col.gameObject.tag == "AI"){
	// 		health -= 5;
	// 		// Scrollbar.size = health/100;
	// 	}
	// }
}
