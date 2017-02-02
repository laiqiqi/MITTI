using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlast : MonoBehaviour {
	public GameObject rock;
	public GameObject pos;
	private float x,y,z;
	private Vector3 movePos;
	private List<GameObject> rocks = new List<GameObject>();
	// Use this for initialization

	void Start () {
		this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		movePos = new Vector3(this.transform.position.x,
								this.transform.position.y + 1f,
								this.transform.position.z);

		for(int i=0; i<100; i++){
			x = Random.Range(-5f, 5f);
			y = Random.Range(15f, 20f);
			z = Random.Range(-5f, 5f);
			Vector3 spawnPos = new Vector3(pos.transform.position.x + x,
										pos.transform.position.y,
										pos.transform.position.z + z);

			GameObject gobj = (GameObject)Instantiate(rock, spawnPos, Quaternion.identity);
			rocks.Add(gobj);
			gobj.GetComponent<Rigidbody>().AddForce(new Vector3(x/2, y, z/2), ForceMode.Impulse);
			gobj.GetComponent<Rigidbody>().angularVelocity = new Vector3(x, y, z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, movePos, 3f);
		
		if (this.transform.localScale != new Vector3(1, 1, 1)) {
			this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		}

		else {
			StartCoroutine(ClearRocks());
			StartCoroutine(ClearSelf());
		}
	}
    
    IEnumerator ClearRocks() {
        yield return new WaitForSeconds(4);
		if(rocks.Count > 0){
			Destroy(rocks[rocks.Count-1].gameObject);
			rocks.RemoveAt(rocks.Count-1);
		}
    }

	IEnumerator ClearSelf() {
        yield return new WaitForSeconds(5);
		if(rocks.Count <= 0){
			Destroy(this.gameObject);
		}
    }
}
