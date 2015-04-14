using UnityEngine;
using System.Collections;

public class picking : MonoBehaviour {

	public GameObject bullet;
	public RaycastHit m_Hit;


	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {


		if ( Input.GetMouseButtonDown(0)){

			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				
				if ( Physics.Raycast(ray,out m_Hit,100000)) {
					
					if( m_Hit.collider.tag == "WALL"){
					GameObject.Find("aim").transform.localPosition = m_Hit.point;
					firebullet();
					}

				}
		}


	} //update last


	
	void firebullet()
	{
		Instantiate( bullet,new Vector3(0,0,0), Quaternion.identity);
	}
}