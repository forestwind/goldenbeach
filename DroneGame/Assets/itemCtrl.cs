using UnityEngine;
using System.Collections;

public class itemCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveBy(gameObject, iTween.Hash("y", 100, "easeType", iTween.EaseType.linear));
	}
	
	// Update is called once per frame
	void Update () {
		//iTween.MoveBy(gameObject, iTween.Hash("y", 100, "easeType", iTween.EaseType.linear));
	}
}
