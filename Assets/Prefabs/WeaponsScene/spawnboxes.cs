using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnboxes : MonoBehaviour {
	// Use this for initialization
	public GameObject box;
	void Start () {
		StartCoroutine(Spawn());
	}
	IEnumerator Spawn(){
		while(true){
			GameObject go = Instantiate(box);
			Rigidbody g = go.GetComponent<Rigidbody>();

			g.velocity = new Vector3(0f, 5f, 0.5f);
			g.useGravity = true;

			Vector3 pos = transform.position;
			pos.x += Random.Range(-1f,1f);
			go.transform.position = pos;

			yield return new WaitForSeconds(1f);
		}
	}
}
