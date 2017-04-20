using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpeningState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	private float speed;
	private int subState;
	private List<GameObject> cubes;
	private float radius;
	public List<AIState> choice{ get;set; }
	private GameObject initialCube;
	private int numCube = 2;
	private Vector3 upPos;

	public OpeningState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.openingState;
		speed = 200;
		subState = -1;
		radius = 2.5f;
		cubes = new List<GameObject> ();
		AI.AICube.gameObject.SetActive(false);
		initialCube = AI.AICube.gameObject;
		initialCube.GetComponent<AICube> ().setHide ();
		upPos = AI.transform.position + (Vector3.up * 5f);
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		if (subState == -1) {
			FloatUp();
		}
		else if (subState == 0) {
			Debug.Log ("testtt");
			for (int i = 0; i < numCube; i++) {
				Vector3 pos = new Vector3 (AI.transform.position.x + Random.Range (-2f, 2f), AI.transform.position.y + Random.Range (-2f, 2f), AI.transform.position.z + Random.Range (-2f, 2f));
				GameObject newcube = GameObject.Instantiate (initialCube, pos, Quaternion.identity) as GameObject;
				newcube.SetActive (true);
//				newcube.transform.GetChild (1).gameObject.SetActive (false);
				newcube.GetComponent<AICube> ().state = 6;
				newcube.gameObject.layer = 9;
				cubes.Add (newcube);
			}
			for (int i = 0; i < 10; i++) {
				Vector3 pos = new Vector3 (AI.transform.position.x + Random.Range (-10f, 10f), AI.transform.position.y + Random.Range (-5f, 5f), AI.transform.position.z + Random.Range (-10f, 10f));
				GameObject newcube = GameObject.Instantiate (initialCube, pos, Quaternion.identity) as GameObject;
				newcube.SetActive (true);
				newcube.transform.GetChild (0).gameObject.SetActive (false);
				newcube.transform.GetChild (1).gameObject.SetActive (true);
				newcube.gameObject.layer = 9;
				cubes.Add (newcube);
			}
			subState = 1;
		}else if(subState == 1){
			int count = 0;
			foreach (GameObject cube in cubes) {
				if (cube.GetComponent<AICube>().isHide) {
					count++;
//					if (cube.transform.GetChild (0).gameObject.activeSelf == false) {
//						GameObject.Destroy (cube.gameObject);
//					} else {
//						GameObject.Destroy (cube.transform.GetChild (1).gameObject);
//					}
				}else if(cube.transform.GetChild (0).gameObject.activeSelf == false){
					count++;
				}
			}

			if (count == cubes.Count) {
				AI.magnet.GetComponent<ContinuousExplosionForce> ().force = -50f;
				subState = 2;
			}
		}else if (subState == 2){
			for (int i = cubes.Count-1; i >= 0; i--) {
				cubes [i].GetComponent<AICube> ().state = 0;
				if (cubes[i].transform.GetChild (0).gameObject.activeSelf == false) {
					GameObject.Destroy (cubes[i].gameObject);
				} else {
					GameObject.Destroy (cubes[i].transform.GetChild (1).gameObject);
				}
			}
			subState = 3;
			EndState();
		}

	}

	void FloatUp()
    {
		if(Vector3.Distance(AI.transform.position, upPos) > 0.1f){
			AI.transform.position = Vector3.MoveTowards(AI.transform.position, upPos, AI.speed * Time.deltaTime);
		}
        else{
			subState = 0;
		}
    }

	public void EndState(){
		Debug.Log("EndOpen");
		AI.NextState();
	}

	public void StateChangeCondition(){

	}
}
