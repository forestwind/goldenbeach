using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	Transform destination;
	Vector3 vector;
	// Use this for initialization
	void Start () {

		destination = GameObject.Find("aim").transform;
		vector=destination.transform.position;
		GetComponent<Rigidbody>().AddForce(vector);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
