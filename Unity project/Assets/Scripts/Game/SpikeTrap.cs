using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

	private GameObject player;
	public int trapDamage = 1;
	public float damageDelay = 1000f;

	private bool timerCheck = false;
	private float timer = 0;
	private Animator anim;

	void Start()
	{
		anim = this.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update()
	{
		if (timerCheck) 
		{
			timer += Time.deltaTime;
		}
		if (timer > damageDelay) 
		{
			timer = 0;
			player.SendMessage ("TakeDamage", trapDamage);
			anim.SetBool ("isTriggered", true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetType () == typeof(PolygonCollider2D) && collision.gameObject.tag == "Player") 
		{
			timerCheck = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetType () == typeof(PolygonCollider2D) && collision.gameObject.tag == "Player") 
		{
			timerCheck = false;
		}
		anim.SetBool ("isTriggered", false);
	}
}
