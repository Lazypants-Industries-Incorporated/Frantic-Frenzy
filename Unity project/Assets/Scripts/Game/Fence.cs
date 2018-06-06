using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

	private Collider2D col;
	private SpriteRenderer sRenderer;

	void Start () 
	{
		col = GetComponent<Collider2D> ();
		sRenderer = GetComponent<SpriteRenderer> ();
	}

	void Appear()
	{
		sRenderer.enabled = true;
		col.enabled = true;
	}

	void Disappear()
	{
		sRenderer.enabled = false;
		col.enabled = false;
	}
}
