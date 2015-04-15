using UnityEngine;
using System.Collections;

public class CubeCtrl : MonoBehaviour {
	void OnCollisionEnter(Collision coll)
	{
		if(coll.collider.tag == "BULLET")
		{
			Destroy(coll.gameObject);
			Destroy(this.gameObject);

			Application.LoadLevel("GameOver");
		}
	}
}
