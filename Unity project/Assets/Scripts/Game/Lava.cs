using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour 
{

	public GameObject player;
	public int lavaDamage = 10;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetType () == typeof(PolygonCollider2D) && collision.gameObject.tag == "Player") 
		{
			player.SendMessage ("TakeDamage", lavaDamage);
		}
	}
}
