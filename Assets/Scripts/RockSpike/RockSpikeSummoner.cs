using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpikeSummoner : MonoBehaviour {
	public GameObject rockSpike;
	private float length = 3.5f;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SummonRockSpikes", 0f, 0.75f);
		// InvokeRepeating("SummonRockSpikes", 0f, 0.5f);
	}

	public void SummonRockSpikes() {
		if(StatePatternAI.instance.health > 0){
			Instantiate(rockSpike, new Vector3(this.transform.position.x + Random.Range(0, length),
										this.transform.position.y + 0,
										this.transform.position.z + Random.Range(0, length)),
										Quaternion.Euler(0, Random.Range(0, 360), 0));
			
			Instantiate(rockSpike, new Vector3(this.transform.position.x + Random.Range(-length, 0),
										this.transform.position.y + 0,
										this.transform.position.z + Random.Range(0, length)),
										Quaternion.Euler(0, Random.Range(0, 360), 0));
			
			Instantiate(rockSpike, new Vector3(this.transform.position.x + Random.Range(-length, 0),
										this.transform.position.y + 0,
										this.transform.position.z + Random.Range(-length, 0)),
										Quaternion.Euler(0, Random.Range(0, 360), 0));
										
			Instantiate(rockSpike, new Vector3(this.transform.position.x + Random.Range(0, length),
										this.transform.position.y + 0,
										this.transform.position.z + Random.Range(-length, 0)),
										Quaternion.Euler(0, Random.Range(0, 360), 0));
			// yield return new WaitForSeconds(3);
		}
    }
}
