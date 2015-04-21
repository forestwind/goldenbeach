using UnityEngine;
using System.Collections;

public class BoxCtrl : MonoBehaviour {


	[System.Serializable]
	public class Anim{
		public AnimationClip open;
	}

	public Anim anim;
	public Animation _animation;

	// Use this for initialization
	void Start () {
		_animation = GetComponent<Animation> ();

		//_animation.clip = anim.open;
		//_animation.Play ();
	}

	void OnCollisionEnter(Collision coll)
	{
		if(coll.collider.tag == "BULLET")
		{
			Debug.Log("Hit");
			_animation.clip = anim.open;
			_animation.Play ();

			Destroy (this.gameObject, 5.0f);
			Destroy(coll.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
