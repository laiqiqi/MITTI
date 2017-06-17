using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpawnMonster : MonoBehaviour {
	private Transform spawnP;
	public GameObject JobMonPrefab;
	
	void Start(){
		if(GameObject.Find("SpawnPoints") != null)
			this.spawnP = GameObject.Find("SpawnPoints").transform;
		else if( spawnP == null || spawnP.childCount > 0)
			Debug.LogError("SpawnPoints object doesnt exist");
	}
	public void SpawnMonsters(){
		// find positions
		for(int j = 0; j < spawnP.childCount; j++){
			Instantiate(JobMonPrefab, spawnP.GetChild(j).transform);
		}
	}

}
