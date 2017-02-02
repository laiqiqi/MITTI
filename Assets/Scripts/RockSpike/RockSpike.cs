using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpike : MonoBehaviour {

	public GameObject stone;
	public GameObject spike;

	private float speed;
	private float x,y,z;
	private List<GameObject> rocks = new List<GameObject>();
	private bool isUp,isDestroy,isCreateStone;
	private Vector3 upPos;

	// Use this for initialization
	void Start () {
		isUp = false;
		isDestroy = false;
		isCreateStone = false;
		speed = 20f;
		upPos = spike.transform.position;
		spike.transform.position = new Vector3(spike.transform.position.x,
											spike.transform.position.y - 5f,
											spike.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isUp && spike.transform.position != upPos) {
			spike.transform.position = Vector3.MoveTowards(spike.transform.position, upPos, speed * Time.deltaTime);
		}
		else{
			if(!isCreateStone){
				CreateSmallStones();
				isCreateStone = true;
			}
			StartCoroutine(IsUpTrue());
		}

		if (isUp) {
			SelfDestruct();
		}
	}

	public void SelfDestruct() {
		if(!isDestroy) {
			for(int i=0; i<30; i++){
				x = Random.Range(-1f, 1f);
				y = Random.Range(1f, 5f);
				z = Random.Range(-1f, 1f);

				Vector3 spawnPos = new Vector3(this.transform.position.x + x,
											this.transform.position.y + y,
											this.transform.position.z + z);
				GameObject gobj = Instantiate(stone, spawnPos, Quaternion.identity);
				rocks.Add(gobj);
				gobj.GetComponent<Rigidbody>().angularVelocity = new Vector3(x, y, z);
			}
			Destroy(spike.gameObject);
			isDestroy = true;
		}
		else{
			StartCoroutine(ClearRocks());
		}
	}

	public void CreateSmallStones() {
		for(int i=0; i<10; i++){
			x = Random.Range(-1f, 1f);
			y = Random.Range(1f, 2f);
			z = Random.Range(-1f, 1f);

			Vector3 spawnPos = new Vector3(this.transform.position.x + x,
										this.transform.position.y + y,
										this.transform.position.z + z);
			GameObject gobj = Instantiate(stone, spawnPos, Quaternion.identity);
			rocks.Add(gobj);
			gobj.GetComponent<Rigidbody>().AddForce(new Vector3(x/2, y*2, z/2), ForceMode.Impulse);
			gobj.GetComponent<Rigidbody>().angularVelocity = new Vector3(x, y, z);
		}
		StartCoroutine(ClearRocksAll());
	}

	IEnumerator ClearRocksAll() {
        yield return new WaitForSeconds(2);
		while(rocks.Count > 0){
			Destroy(rocks[rocks.Count-1].gameObject);
			rocks.RemoveAt(rocks.Count-1);
		}
    }

	IEnumerator ClearRocks() {
        yield return new WaitForSeconds(2);
		if(rocks.Count > 0){
			Destroy(rocks[rocks.Count-1].gameObject);
			rocks.RemoveAt(rocks.Count-1);
		}
		else{
			Destroy(this.gameObject);
		}
    }

	IEnumerator IsUpTrue() {
        yield return new WaitForSeconds(1);
		isUp = true;
    }
}
