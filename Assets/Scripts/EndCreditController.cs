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
		period = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(alpha < 1){
			alpha += Time.deltaTime * 0.5f;
			gameName.color = new Color(1, 1, 1, alpha);
		}
		else if(!isWait){
			if(canvas.GetComponent<RectTransform>().offsetMin.y < period){
				if(canvas.GetComponent<RectTransform>().offsetMin.y < 10800f){
					y += 100f;
				}
				else{
					y += 3f;
				}
				canvas.GetComponent<RectTransform>().offsetMin = new Vector2(0, y);
			}
			else{
				if(period == 0f){
					Debug.Log("Wait 5");
					StartCoroutine(WaitForSec(5f));
				}
				else{
					StartCoroutine(WaitForSec(3f));
				}
			}
		}
	}

	IEnumerator WaitForSec(float sec){
		isWait = true;
		yield return new WaitForSeconds(sec);
		isWait = false;
		if(period < 1800f){
			period = 1800f;
		}
		else if(period < 3600f){
			period = 3600f;
		}
		else if(period < 5400f){
			period = 5400f;
		}
		else if(period < 7200f){
			period = 7200f;
		}
		else if(period < 9000f){
			period = 9000f;
		}
		else if(period < 10800f){
			period = 10800f;
		}
		else if(period < 13800f){
			period = 13800f;
		}
		else{
			isEnd = true;
			// Debug.Log(isEnd);
		}
	}
}
