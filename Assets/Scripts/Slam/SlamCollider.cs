using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamCollider : MonoBehaviour {
	public Collider colInfo;

	void OnTriggerEnter(Collider collider) {
		colInfo = collider;
	}
}
