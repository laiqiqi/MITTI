using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCreditController : MonoBehaviour {

	public GameObject canvas;
	public Image gameName;

	private float alpha, y, period = 0;
	public bool isWait, isEnd = false;

	// Use this for initialization
	void Start () {
		period = 1400f;
	}
	
	// Update is called once per frame
	void Update () {

		if(alpha < 1){
			alpha += Time.deltaTime * 0.5f;
			gameName.color = new Color(1, 1, 1, alpha);
		}
		else if(!isWait){
			if(canvas.GetComponent<RectTransform>().offsetMin.y < period){
				if(canvas.GetComponent<RectTransform>().offsetMin.y < 8400){
					y += 150f;
				}
				else{
					y += 3f;
				}
				canvas.GetComponent<RectTransform>().offsetMin = new Vector2(0, y);
			}
			else{
				StartCoroutine(WaitForSec(1f));
			}
		}
	}

	IEnumerator WaitForSec(float sec){
		isWait = true;
		yield return new WaitForSeconds(sec);
		isWait = false;
		if(period < 2800f){
			period = 2800f;
		}
		else if(period < 4200f){
			period = 4200f;
		}
		else if(period < 5600f){
			period = 5600f;
		}
		else if(period < 7000f){
			period = 7000f;
		}
		else if(period < 8400f){
			period = 8400f;
		}
		else if(period < 11400f){
			period = 11400f;
		}
		else{
			isEnd = true;
			// Debug.Log(isEnd);
		}
	}
}
