using UnityEngine;
using System.Collections;

public class InstantGuiButton : InstantGuiElement
{
	public InstantGuiActivator onPressed;
	public string SceneName;
	public override void Action ()
	{
		base.Action();
		if (activated) onPressed.Activate (this);

	
	}

	public void OnMouseDown(){
		Application.LoadLevel (SceneName);

	}
}
