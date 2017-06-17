using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTutorialMinions : MonoBehaviour {

	// Use this for initialization
	public float frequency, magnitude;
	private TutorialArrowAI TutorialAI;
	void Start () {
		TutorialAI = transform.root.GetComponent<TutorialArrowAI>();
		Debug.Log(transform.root);
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.right * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log($"This is col of colider {col.tag}");
		// if (col.tag.Equals("projectile") && !isTutor) {
		// 	Debug.Log("EndTutor");
		// 	isEndTutor = true;
		// }
		if (col.tag.Equals("projectile")){
			TutorialAI.OnMinionHit();
			Destroy(gameObject);
		}
	}
}
