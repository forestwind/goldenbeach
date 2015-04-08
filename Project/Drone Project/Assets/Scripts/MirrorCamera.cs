using UnityEngine;
using System.Collections;

public class MirrorCamera : MonoBehaviour {

	void OnPreCull () {
		GetComponent<Camera>().ResetWorldToCameraMatrix ();
		GetComponent<Camera>().ResetProjectionMatrix ();
		GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix * Matrix4x4.Scale(new Vector3 (-1, 1, 1));
	}
	
	void OnPreRender () {
		GL.SetRevertBackfacing (true);
	}
	
	void OnPostRender () {
		GL.SetRevertBackfacing (false);
	}
	
	//    @script RequireComponent (Camera)
}
