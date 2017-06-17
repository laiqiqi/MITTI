using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FloorFollow : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.transform.position = Player.instance.transform.position;
	}
}
