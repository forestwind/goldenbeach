using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	Vector3 destination;
	public int speed=10000;

	// Use this for initialization
	void Start () {
		destination = GameObject.Find("DroneCamera").GetComponent<Stage2BulletCtrl>().m_Hit.point;
		transform.LookAt(destination);



	}
	
	// Update is called once per frame
	void Update () {


	




		transform.Translate ( Vector3.forward * Time.deltaTime * speed);


		if(transform.position.z > 1000)
			Destroy(this.gameObject);

	}
}
