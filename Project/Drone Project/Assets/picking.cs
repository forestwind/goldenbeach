using UnityEngine;
using System.Collections;

public class picking : MonoBehaviour {

	public GameObject bullet;
	public RaycastHit m_Hit;
	public Vector3 point;
	Vector3 initpoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if ( Input.GetMouseButtonDown(0)){


			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				if ( Physics.Raycast(ray,out m_Hit,100000))
			    {
					GameObject.Find("aim").transform.localPosition = m_Hit.point;
					point=m_Hit.point;
					//Debug.Log(m_Hit.point);
					firebullet(m_Hit.point);
			    }
		}
	}


	void firebullet(Vector3 destination)
	{
		initpoint=GetComponent<Camera>().transform.position;
		initpoint.z-=100;
		Instantiate( bullet,initpoint, Quaternion.identity);
	}
}