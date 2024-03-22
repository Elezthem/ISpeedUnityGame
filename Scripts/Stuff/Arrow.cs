using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float scrollSpeed = 0.5f;
	Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(rend)
		{
		float offset = Time.time * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2(offset, 0);
		}
	}
}
