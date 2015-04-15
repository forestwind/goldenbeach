using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {
	public GameObject bullet;
	public Transform firePos;


	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("l")){
			Fire ();
		}
	}

	void Fire(){
		StartCoroutine (this.CreateBullet ());
	}

	IEnumerator CreateBullet(){
		Instantiate (bullet, firePos.position, firePos.rotation);
		yield return null;
	}
}
