using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class GameUI : MonoBehaviour {
	public Text timeText;
	public float _time;

	// Use this for initialization
	void Start () {
		_time = 10.0f;	
	}
	
	// Update is called once per frame
	void Update () {
		_time -= Time.deltaTime;
		timeText.text = _time.ToString("N1");
		}

}
