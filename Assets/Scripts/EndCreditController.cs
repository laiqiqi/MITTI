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
		period = 625f;
	}
	
	// Update is called once per frame
	void Update () {

		if(alpha < 1){
			alpha += Time.deltaTime * 0.5f;
			gameName.color = new Color(1, 1, 1, alpha);
		}
		else if(!isWait){
			if(canvas.GetComponent<RectTransform>().offsetMin.y < period){
				if(canvas.GetComponent<RectTransform>().offsetMin.y < 3600){
					y += 4f;
				}
				else{
					y += 1f;
				}
				canvas.GetComponent<RectTransform>().offsetMin = new Vector2(0, y);
			}
			else{
				StartCoroutine(WaitForSec(3f));
			}
		}
	}

	IEnumerator WaitForSec(float sec){
		isWait = true;
		yield return new WaitForSeconds(sec);
		isWait = false;
		if(period < 1200f){
			period = 1200f;
		}
		else if(period < 1850f){
			period = 1850f;
		}
		else if(period < 2450f){
			period = 2450f;
		}
		else if(period < 3050f){
			period = 3050f;
		}
		else if(period < 3600f){
			period = 3600f;
		}
		else if(period < 5600f){
			period = 5600f;
		}
		else{
			isEnd = true;
			// Debug.Log(isEnd);
		}
	}
}
