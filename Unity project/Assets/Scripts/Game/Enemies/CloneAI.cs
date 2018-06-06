using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAI : MonoBehaviour {

	public float speed;
	public Transform[] targets;
   

    private Transform currentTarget;
	private Animator anim;
	private Vector2 lastMove;
	private bool isMoving;
	private float distanceToObject;
	private SpriteRenderer spriteRenderer;

	void Start () 
	{
       
        anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		lastMove = new Vector2(transform.position.x, transform.position.y);

		currentTarget = null;
	}

	void Update () 
	{
		if (currentTarget != null) 
		{
			transform.position = Vector2.MoveTowards (transform.position, currentTarget.position, speed * Time.deltaTime);
			distanceToObject = Vector2.Distance (transform.position, currentTarget.position);

			if (distanceToObject > 0) 
			{
				isMoving = true;

				anim.SetFloat ("MoveX", transform.position.x - lastMove.x);
				anim.SetFloat ("MoveY", transform.position.y - lastMove.y);
				anim.SetFloat ("LastMoveX", transform.position.x - lastMove.x);
				anim.SetFloat ("LastMoveY", transform.position.y - lastMove.y);
				lastMove.Set (transform.position.x, transform.position.y);
			} else 
			{
				isMoving = false;
				SetCurrenttarget ();
			}
			anim.SetBool ("IsMoving", isMoving);
		} 
		else SetCurrenttarget ();

	}
	void SetCurrenttarget()
	{
		if(!targets[0].GetComponent<PowerPlug>().isPlugged)
		{
			currentTarget = targets [0];
		}
		else if (!targets[1].GetComponent<PowerPlug>().isPlugged)
		{
			currentTarget = targets[1];
		}
		else if (!targets[2].GetComponent<PowerPlug>().isPlugged)
		{
			currentTarget = targets[2];
		}
		else currentTarget = null;
	}
}
