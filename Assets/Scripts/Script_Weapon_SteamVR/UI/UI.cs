using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	// Use this for initialization
	Animator animator;
	void Start () {
		animator = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){
			// int hash = Animator.StringToHash("Enlarge");
			// animator.Play(hash, 0, 0);
			animator.SetTrigger("On");
		}
            // animator.SetTrigger("On");
			
		if (Input.GetKeyUp("space"))
            animator.SetTrigger("Idle");
        
	}
}
