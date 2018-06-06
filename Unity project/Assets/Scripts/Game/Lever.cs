using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	Animator anim;
	GameObject player;
	GameObject door;
    public AudioClip doorSound;
    AudioSource audio;

    void Start () 
	{
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		door = GameObject.Find ("DoubleDoor");
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E) && Vector2.Distance (transform.position, player.transform.position) < 1) 
		{
			anim.SetBool ("isTriggered", true);
            audio.PlayOneShot(doorSound);
            door.SendMessage ("Open");
		}
	}
}
