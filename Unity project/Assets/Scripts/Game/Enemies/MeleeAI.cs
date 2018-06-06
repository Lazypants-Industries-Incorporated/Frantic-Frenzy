using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{

    public Transform target; //Player transform
    public float speed;
    public float attackRange;
    public float chaseRange;
    public float attackDelay;
    public int health;
    public int damage;
    public AudioClip swordSound;
    public AudioClip takeDmgSound;
    AudioSource audio;

    private SpriteRenderer sprite;
    private Animator anim;
    private Vector2 lastMove;
    private float lastAttack;
    private bool isMoving;
    private float distanceToPlayer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        audio = GetComponent<AudioSource>();    
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        lastMove = new Vector2(transform.position.x, transform.position.y);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //It's for making enemy red after getting damage
    }

    void Update()
    {
        //Prevents head from going up the player's butt
        if (transform.position.y < target.position.y)
            sprite.sortingOrder = 4;
        else
            sprite.sortingOrder = 2;

        isMoving = false;
        distanceToPlayer = Vector2.Distance(transform.position, target.position);

        //Attack player if within attack range
        if (distanceToPlayer < attackRange && Time.time > lastAttack + attackDelay)
        {
            audio.PlayOneShot(swordSound);
            anim.SetTrigger("AttackTrigger");
            target.SendMessage("TakeDamage", damage);
            lastAttack = Time.time;
        }
        //Otherwise, chase player if within chase range
        else if (distanceToPlayer < chaseRange && distanceToPlayer > attackRange)
        {
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //Update positions for anim
            anim.SetFloat("MoveX", transform.position.x - lastMove.x);
            anim.SetFloat("MoveY", transform.position.y - lastMove.y);
            anim.SetFloat("LastMoveX", transform.position.x - lastMove.x);
            anim.SetFloat("LastMoveY", transform.position.y - lastMove.y);
            lastMove.Set(transform.position.x, transform.position.y);
        }

        anim.SetBool("IsMoving", isMoving);

    }

    public void TakeDamage(int damageValue)
    {
        audio.PlayOneShot(takeDmgSound);
        health -= damageValue;
        Debug.Log("Taking damage of value: " + health);

        switch (health)
        {
            case 2:
                spriteRenderer.color = new Color32(255, 118, 118, 255);
                break;
            case 1:
                spriteRenderer.color = new Color32(255, 41, 41, 255);
                break;
        }

        if (health <= 0)
            Destroy(gameObject);
    }
}
