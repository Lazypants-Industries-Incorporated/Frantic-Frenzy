using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip bossFightMusic;
	private GameObject doubleDoor;
	private bool isTriggered = false;

	void Start () 
	{
		audioSource = GameObject.Find ("GameController").GetComponent<AudioSource> ();
		doubleDoor = GameObject.Find ("DoubleDoor");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!isTriggered)// do not replay music
		{
			if (col.GetType () == typeof(PolygonCollider2D) && col.gameObject.tag == "Player") 
			{
				audioSource.clip = bossFightMusic;
				audioSource.Play ();
				doubleDoor.SendMessage ("Close");

				DestroyAllEnemiesWithTag ("EnemyMelee");
				DestroyAllEnemiesWithTag ("EnemyRanged");

				isTriggered = true;
			}
		}
	}
	private void DestroyAllEnemiesWithTag(string objectTag)
	{
		try
		{
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (objectTag);
			for (var i = 0; i < gameObjects.Length; i++) 
			{
				Destroy (gameObjects [i]);
			}
		}
		catch{}
	}
}
