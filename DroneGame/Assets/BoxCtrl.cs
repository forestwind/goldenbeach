using UnityEngine;
using System.Collections;

public class BoxCtrl : MonoBehaviour {

	//public GameObject item;
	private itemCtrl items;

	
	[System.Serializable]
	public class Anim{
		public AnimationClip open;
	}

	public Anim anim;
	public Animation _animation;

	// Use this for initialization
	void Awake () {

		items = GameObject.Find ("Capsule").GetComponent<itemCtrl> ();
		items.enabled = false;

	}

	void Start(){
		_animation = GetComponent<Animation> ();
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

			StartCoroutine(itemUp());

		}
	}

	IEnumerator itemUp(){
		yield return new WaitForSeconds(2.0f);
		items.enabled = true;
	}
	// Update is called once per frame
	void Update () {

	}
}
