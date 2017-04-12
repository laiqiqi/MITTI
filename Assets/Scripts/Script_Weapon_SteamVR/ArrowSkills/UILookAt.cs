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
		Hand otherhand = GetComponentInParent<Hand>().otherHand;
		this.transform.position = otherhand.transform.position;
		this.transform.parent = otherhand.transform;
		head = InteractionSystem.Player.instance.audioListener;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(head);
	}
}
}
