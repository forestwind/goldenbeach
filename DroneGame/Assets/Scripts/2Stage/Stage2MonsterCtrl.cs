using UnityEngine;
using System.Collections;

using System;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;


public class Stage2MonsterCtrl : MonoBehaviour {

	
	NyARUnityMarkerSystem _ms;
	public GameObject monster;



	float timer;
	int GenerationTime;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		GenerationTime = 1;
		_ms=GameObject.Find("DroneCamera").GetComponent<DroneController>()._ms;

	
	}
	
	// Update is called once per frame
	void Update () {

		if(this._ms.isExistMarker(0)){
			timer += 0.05f;
			if(timer > GenerationTime)
			{
				float x = UnityEngine.Random.Range(-100f,100f);
				float y = UnityEngine.Random.Range(-100f,100f);
				Vector3 pos = GameObject.Find("MarkerObject").transform.localPosition;
				Debug.Log("make");
				Instantiate(monster,new Vector3( pos.x - x ,pos.y - y , pos.z),Quaternion.identity);
				timer = 0;
			}

		}
	
	}
}
