using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {

	// Use this for initialization
	private bool isStartPopUp;
	private Text text;
	private float width, height;
	void Start () {
		isStartPopUp = false;
		text = transform.FindChild("Text").GetComponent<Text>();
		width = text.GetComponent<Text>().preferredWidth;
		height = text.GetComponent<Text>().preferredHeight;
		Debug.Log(width + " " + height);
	}
	
	// Update is called once per frame
	void Update () {
		DoPopUp();
	}

	void DoPopUp() {
		if (isStartPopUp) {
			// if () {

			// }
		}
	}

	public void SetPopUp(bool start){
		isStartPopUp = start;
	}
}
