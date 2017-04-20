using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinuousExplosionForce : MonoBehaviour
{
	public float force = -100.0f;
	public float radius = 50f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;
	public int size = 20;
	private List<GameObject> magnetObjList;

	// Use this for initialization
	void Start () {
		magnetObjList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		foreach(Collider col in Physics.OverlapSphere(transform.position, radius))
		{
			if(col.GetComponent<Rigidbody>() != null && col.transform.tag == "Magnet")
			{
				col.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position,radius,upwardsModifier,forceMode);
				if (magnetObjList.Count < size) {
					magnetObjList.Add (col.gameObject);
					// col.transform.parent = this.transform;
				}
				else if (magnetObjList.Count > size) {
					magnetObjList.Remove (col.gameObject);
					// col.transform.parent = null;
				}
			}
		}
	}
}
