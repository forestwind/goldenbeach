using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Camera))]
public class mouse : MonoBehaviour {
	
	Vector3 initMousePos;
	Vector3 initMousePos1;

	void Update() {

	
	}

	void OnMouseDown() {

		initMousePos=Input.mousePosition;
		initMousePos.z = 10000;
		initMousePos.y = -initMousePos.y;



		Debug.Log(initMousePos);


		GameObject.Find("Cubee").transform.localPosition = initMousePos;


	}

}
