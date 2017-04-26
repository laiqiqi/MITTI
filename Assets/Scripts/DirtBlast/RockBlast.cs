using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class RockBlast : MonoBehaviour {
	private Vector3 movePos;
	private bool isHit, isHasDamage;
	private GameObject environment;
	public ParticleSystem RockDebris1, RockDebris2;
	// Use this for initialization
	void Start () {
		isHit = false;
		isHasDamage = true;
		this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		environment = GameObject.Find("EnvironmentSmaller");

		movePos = new Vector3(this.transform.position.x,
								this.transform.position.y + 1f,
								this.transform.position.z);

		AssignDebrisCollision();
	}
	
	// Update is called once per frame
	void Update () {
		if(isHasDamage == true){
			this.transform.position = Vector3.MoveTowards(this.transform.position, movePos, 3f);
		}
		
		if (this.transform.localScale.x < 1f) {
			this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		}

		else {
			if (isHasDamage == true){
				isHasDamage = false;
				StartCoroutine(ClearSelf());
			}
			this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - (Vector3.up), 0.005f);
		}
	}

	// void OnCollisionEnter (Collision col) {
	// 	// Debug.Log(col.collider.tag);
	// 	if(col.collider.tag == "Player" && !isHit && isHasDamage){
	// 		isHit = true;
	// 		Debug.Log(col.collider.tag);
	// 		Player.instance.GetComponent<PlayerStat>().health -= 50f;
	// 	}
	// }

	void OnTriggerEnter (Collider col)  {
		if(col.tag.Equals("Player") && !isHit && isHasDamage){
			isHit = true;
			Player.instance.GetComponent<PlayerStat>().PlayerTakeDamage(50f);
		}
	}

	IEnumerator ClearSelf() {
        yield return new WaitForSeconds(5);
		Destroy(this.gameObject);
    }

	void AssignDebrisCollision(){
		RockDebris1.collision.SetPlane(0, environment.transform.FindChild("Floor").transform);
		RockDebris2.collision.SetPlane(0, environment.transform.FindChild("Floor").transform);
	}
}
