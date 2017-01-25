using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ToolUser1 : MonoBehaviour {


	public Material capMaterial;

	// Use this for initialization
	
	void OnCollisionEnter(Collision colliderz){


				GameObject victim = colliderz.collider.gameObject;

				GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

				if(!pieces[1].GetComponent<Rigidbody>())
					pieces[1].AddComponent<Rigidbody>();

				Destroy(pieces[1], 1);

		
	}

}
