using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	private Animator anim;
	private GameObject player;
	private SpriteRenderer sRenderer;
	private AudioSource audio;

	public Sprite tableNoFood;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		sRenderer = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E) && Vector2.Distance (transform.position, player.transform.position) < 2) 
		{
			if (sRenderer.sprite != tableNoFood) 
			{
				sRenderer.sprite = tableNoFood;
				audio.Play ();
				player.SendMessage ("HealUp", 1);
			}
		}	
	}
}
