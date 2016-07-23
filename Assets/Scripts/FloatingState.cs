using UnityEngine;
using System.Collections;

public class FloatingState : IEnemyState 

{
	private readonly StatePatternEnemy enemy;
	private int nextWayPoint;
	private Vector3 destination;
	private float t;

	public FloatingState (StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}

	public void UpdateState()
	{
		Look ();
		Floating ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			Debug.Log ("Alert");
//			ToAlertState ();
	}

	public void ToPatrolState()
	{
		Debug.Log ("Can't transition to same state");
	}

	public void ToAlertState()
	{
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState()
	{
		enemy.currentState = enemy.chaseState;
	}

	private void Look()
	{
//		RaycastHit hit;
//		if (Physics.Raycast (enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag ("Player")) {
//			enemy.chaseTarget = hit.transform;
//			ToChaseState();
//		}
		enemy.transform.LookAt(enemy.player.transform);
	}

	void Floating ()
	{
		Debug.Log ("1111111111111");
		enemy.meshRendererFlag.material.color = Color.white;
		Debug.Log ("Floating");
		if (destination == null) {
			Vector3 randomVector = RandomVector ();
			destination = enemy.transform.position + randomVector;
			t = 0;
		}
		t += 0.01f;
		enemy.transform.position = Vector3.Lerp (enemy.transform.position, destination, t);
		if (enemy.transform.position.y <= 1) {
			enemy.transform.position = enemy.transform.position - new Vector3 (0, enemy.transform.position.y + 1, 0);
//			enemy.transform.position.y = 1f;
		}

		if (t >= 1) {
			Vector3 randomVector = RandomVector ();
			destination = enemy.transform.position + randomVector;
			t = 0;
		}
//		enemy.navMeshAgent.destination = enemy.wayPoints [nextWayPoint].position;
//		enemy.navMeshAgent.Resume ();

//		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending) {
//			nextWayPoint =(nextWayPoint + 1) % enemy.wayPoints.Length;
//
//		}


	}

	Vector3 RandomVector (){
		Debug.Log ("aaaaaaaaaaaaaaa");
		Vector3 randomVector = Vector3.zero;
		Vector3 playerVector = enemy.player.transform.position;
		float distanceX = enemy.transform.position.x - enemy.player.transform.position.x;
		float distanceY = enemy.transform.position.y - enemy.player.transform.position.y;
		float distanceZ = enemy.transform.position.z - enemy.player.transform.position.z;
		randomVector = randomVector = new Vector3 ((float)Random.Range (-30f - distanceX, 30f - distanceX), 
			(float)Random.Range (-30f - distanceY, 30f - distanceY), 
			(float)Random.Range (-30f - distanceZ, 30f - distanceZ));
//		while (!(Vector3.Distance(enemy.player.transform.position, randomVector) < 10f || randomVector == Vector3.zero)){
//			randomVector = new Vector3 ((float)Random.Range (-30f, 30f), (float)Random.Range (-30f, 30f), (float)Random.Range (-30f, 30f));
//			if(enemy.transform.position.y + randomVector.y <=0 ){
//				continue;
//			}
//			Debug.Log (Vector3.Distancey
//		}

		return randomVector;
	}
}