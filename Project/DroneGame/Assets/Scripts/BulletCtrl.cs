using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {

	public int speed = 10;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (Vector3.forward * speed);
	}
}
