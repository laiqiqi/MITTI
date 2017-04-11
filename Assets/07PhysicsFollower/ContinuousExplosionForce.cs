﻿using UnityEngine;
using System.Collections;

public class ContinuousExplosionForce : MonoBehaviour
{
	public float force = 100.0f;
	public float radius = 5.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		foreach(Collider col in Physics.OverlapSphere(transform.position, radius))
		{
			if(col.GetComponent<Rigidbody>() != null)
			{
				col.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position,radius,upwardsModifier,forceMode);
			}
		}
	}
}
