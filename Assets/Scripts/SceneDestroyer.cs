using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneDestroyer : MonoBehaviour
{
	public float force = -100.0f;
	public float radius = 50f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;
	public int size = 20;
	public float speed;

	private List<GameObject> magnetObjList;
	private Vector3 maxPos, minPos;
	private bool moveFront, incForce;

	// Use this for initialization
	void Start () {
		magnetObjList = new List<GameObject>();
		// maxPos = transform.forward*15f + transform.right*15f;
		// minPos = transform.forward*-15f - transform.right*15f;
		maxPos = transform.right*15f;
		minPos = transform.right*-15f;
		moveFront = true;
		incForce = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		foreach(Collider col in Physics.OverlapSphere(transform.position, radius))
		{
			if(col.GetComponent<Rigidbody>() != null && col.transform.tag == "SceneProps")
			{
				col.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position,radius,upwardsModifier,forceMode);
				if (magnetObjList.Count < size) {
					magnetObjList.Add (col.gameObject);
					col.transform.parent = this.transform;
				}
				else if (magnetObjList.Count > size) {
					magnetObjList.Remove (col.gameObject);
					col.transform.parent = null;
				}
			}
		}
		
		if(moveFront){
			transform.position = Vector3.MoveTowards(transform.position, maxPos, speed*Time.fixedDeltaTime);
			if(Vector3.Distance(transform.position, maxPos) < 0.1f){
				moveFront = !moveFront;
			}
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, minPos, speed*Time.fixedDeltaTime);
			if(Vector3.Distance(transform.position, minPos) < 0.1f){
				moveFront = !moveFront;
			}
		}

		if(force < 100 && incForce){
			force += 10f;
		}
		else{
			force -= 5f;
			incForce = false;
			if(force < -100){
				incForce = true;
			}
		}
	}
}
