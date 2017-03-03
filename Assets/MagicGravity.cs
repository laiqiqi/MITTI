// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using VRTK;
// public class MagicGravity : MonoBehaviour {
// 	private List<GameObject> ObjectList;
// 	// Use this for initialization
// 	private VRTK_ControllerEvents controllerEvents;
// 	private bool pressed = false;

// 	void Start () {
// 		controllerEvents = GetComponent<VRTK_ControllerEvents>();
// 		ObjectList = CreateList();
// 		if (GetComponent<VRTK_ControllerEvents>() == null)
//             {
//                 Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
//                 return;
//             }
// 			GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
//             GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
// 	}
// 	private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
//         {
//             Debug.Log("Controller on index '" + index + "' " + button + " has been " + action
//                     + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
//         }
// 	private List<GameObject> CreateList(){
// 		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
// 		var goList = new List<GameObject>();
// 		for (var i = 0; i < goArray.Length; i++) {
// 			// Debug.Log(goArray[i].gameObject.name);
// 			if (goArray[i].layer == 8) {
// 				goList.Add(goArray[i]);
// 			}
// 		} 
// 		return goList;
// 	}
	

// 	private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
//         {
//             DebugLogger(e.controllerIndex, "TRIGGER", "pressed", e);
// 			pressed = true;
//         }

// 	private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
// 	{
// 		// DebugLogger(e.controllerIndex, "TRIGGER", "released", e);
// 		pressed = false;
// 	}

// 	private void StarWarForceShit(GameObject g){
// 		g.GetComponent<Rigidbody>().AddForce(controllerEvents.GetVelocity().normalized * Random.Range(1.5f, 2f));
// 	}

// 	// Update is called once per frame
// 	void Update () {
// 		if(pressed){
// 			foreach (GameObject go in ObjectList){
// 			// Debug.Log(go.gameObject.name);
// 			StarWarForceShit(go);
// 			}
// 		}
// 	}
// }
