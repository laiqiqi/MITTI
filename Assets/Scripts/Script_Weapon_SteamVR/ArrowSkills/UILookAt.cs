using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
public class UILookAt : MonoBehaviour {
	private Transform head;
	// Use this for initialization
	void Start () {
		//audiolistener uses transform of head
		//get hand 
		//move to other hand
		
		head = InteractionSystem.Player.instance.audioListener;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(head);
	}
}
}
