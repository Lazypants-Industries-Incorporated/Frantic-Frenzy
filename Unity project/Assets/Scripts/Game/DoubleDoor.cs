using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour {

	private Animator anim;
	private GameObject player;
	private Collider2D col;

	void Start () 
	{
		anim = GetComponent<Animator> ();	
		player = GameObject.FindGameObjectWithTag ("Player");
		col = GetComponent<Collider2D> ();
	}
	void Open()
	{
		anim.SetBool ("isTriggered", true);
		col.enabled = false;
	}
	void Close()
	{
		anim.SetBool ("isTriggered", false);
		col.enabled = true;
	}
}
