using UnityEngine;
using System.Collections;

public class Stage2MonsterCtrl1 : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().AddForce(0,0,-speed);
	}
}
