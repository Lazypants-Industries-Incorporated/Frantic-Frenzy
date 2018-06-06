using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {

    public GameObject player;
    public bool bottomAngle;
    private AudioSource audioSource;
    float x, y;
    private int count = 0;
    private bool beingHandled = false;
    public int damage;
    //hell
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(PolygonCollider2D) && collision.gameObject.tag == "Player")
        {
            player.SendMessage("TakeDamage", damage);
            if (bottomAngle) transform.position = new Vector3(-31, 34, 0);
            else transform.position = new Vector3(-43, 24, 0);
            count++;
        }
        
        if (collision.GetType() == typeof(BoxCollider2D) && collision.gameObject.tag != "Player")
        {
            if (count == 0)
            {
                if (bottomAngle) transform.position = new Vector3(-31, 34, 0);
                else transform.position = new Vector3(-43, 24, 0);
                count++;
            }
            else
            {
                transform.position = new Vector3(x, y, 0);
                audioSource.Play();
                count = 0;
            }
        }       
    }
    void Update()
    {
        if (!beingHandled)
        {
            StartCoroutine(waiter());                       
        }
        
    }
    IEnumerator waiter()
    {
        beingHandled = true;
        if (bottomAngle)
        {          
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, 0);
            yield return new WaitForSeconds(0.01f);           
        }
        else
        {          
            transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, 0);
            yield return new WaitForSeconds(0.01f);            
        }
        beingHandled = false;
    }   
}
