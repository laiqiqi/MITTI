﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	// Use this for initialization
	public Camera targetCamera;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(targetCamera.transform);
	}
}
