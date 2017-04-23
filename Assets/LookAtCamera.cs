using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
public class LookAtCamera : MonoBehaviour {

	// Use this for initialization
	public Transform targetCamera;
	void Start () {
		 targetCamera = Player.instance.hmdTransforms[0];
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(targetCamera.transform);
	}
}
}
