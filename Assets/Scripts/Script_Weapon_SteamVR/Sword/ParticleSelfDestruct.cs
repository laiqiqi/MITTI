using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour {

	// Use this for initialization
	public float SecsToDestroy;
	void Start () {
		StartCoroutine(SelfDestruct());
	}
	IEnumerator SelfDestruct(){
		yield return new WaitForSeconds(SecsToDestroy);
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
