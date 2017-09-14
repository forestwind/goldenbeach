﻿using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public void OnClickStartBtn(RectTransform rt)
	{
		//Debug.Log ("Scale X : "+ rt.localScale.x.ToString());

		Application.LoadLevel ("Play");
	}

	public void OnClickExitBtn(RectTransform rt)
	{
		Debug.Log ("EXIT");
		Application.Quit ();
	}

}
