using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAISword : MonoBehaviour {
	public Transform target;
	public float speed;
	public GameObject player;
	public int count;

	// Use this for initialization
	void Start () {
		target = new GameObject ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (count == 0) {
			Floating ();
		} else if(count ==1){
			StartCoroutine(Example());
		}else{
			Slash ();
		}
	}

	void OnCollisionEnter(Collision coll){
		count = 1;
//		Vector3 dir = coll.transform.position - transform.position;
//		coll.rigidbody.AddForce(dir.normalized * 500);
	}

	IEnumerator Example() {
//		print(Time.time);
		yield return new WaitForSeconds(5);
		count = 0;
	}

	void Floating(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		if(Vector3.Distance(transform.position, target.position) < 0.1f){
			//It is within ~0.1f range, do stuff
			target.position = new Vector3 (player.transform.position.x + Random.Range (-10f, 10f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				Random.Range (0f, 10f),
				player.transform.position.z + Random.Range (-10f, 10f));

		}
		if(Vector3.Distance(transform.position, player.transform.position) < 4f){
			target.position = new Vector3 (player.transform.position.x + Random.Range (-10f, 10f),
				//				this.transform.position.y + Random.Range (-10f, 10f),
				Random.Range (0f, 10f),
				player.transform.position.z + Random.Range (-10f, 10f));
			count = 2;
			transform.LookAt(-player.transform.position);
		}
		transform.LookAt(player.transform);
	}

	void Slash(){
		transform.Rotate (Vector3.up * 300f * Time.deltaTime);
	}
}
