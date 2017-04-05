using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RockSpike : MonoBehaviour {

	public ParticleSystem rockSpDebris, rockSpUpDebris;
	public GameObject spike;

	private float speed;
	private bool isUp,isDestroy,isCreateStone,isHit;
	private Vector3 upPos;
	private GameObject environment;

	// Use this for initialization
	void Start () {
		environment = GameObject.Find("EnvironmentSmaller");
		isHit = false;
		isUp = false;
		isDestroy = false;
		isCreateStone = false;
		speed = 15f;
		upPos = spike.transform.position;
		spike.transform.position = new Vector3(spike.transform.position.x,
											spike.transform.position.y - 5f,
											spike.transform.position.z);
		AssignDebrisCollision();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isUp && Vector3.Distance(spike.transform.position, upPos) > 0.1f) {
			spike.transform.position = Vector3.MoveTowards(spike.transform.position, upPos, speed * Time.deltaTime);
		}
		else if (!isUp){
			if(!isCreateStone){
				isCreateStone = true;
			}
			StartCoroutine(IsUpTrue());
		}

		if (isUp) {
			SelfDestruct();
		}
	}

	void OnCollisionEnter(Collision col) {
		if(col.collider.tag == "Player" && isHit == false){
			isHit = true;
			Debug.Log(col.collider.tag);
			Player.instance.GetComponent<PlayerStat>().health -= 10f;
		}
	}

	public void SelfDestruct() {
		if(!isDestroy) {
			rockSpDebris.gameObject.SetActive(true);
			this.GetComponent<CapsuleCollider>().enabled = false;
			Destroy(spike.gameObject);
			isDestroy = true;
		}
		else{
			StartCoroutine(ClearSelf());
		}
	}

	IEnumerator IsUpTrue() {
        yield return new WaitForSeconds(1);
		isUp = true;
    }

	IEnumerator ClearSelf() {
		yield return new WaitForSeconds(3);
		Destroy(this.gameObject);
	}

	void AssignDebrisCollision(){
		rockSpDebris.collision.SetPlane(0, environment.transform.FindChild("Floor").transform);
		rockSpUpDebris.collision.SetPlane(0, environment.transform.FindChild("Floor").transform);
	}
}
