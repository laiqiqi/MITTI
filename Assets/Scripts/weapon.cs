﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = Input.mousePosition;
//		temp.z = 5f; // Set this to be the distance you want the object to be placed in front of the camera.
		this.transform.position = Camera.main.ScreenToWorldPoint(temp);
	}
}
